using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Microsoft.Maps.MapControl;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

namespace TwitterRx
{
  public partial class MainPage : UserControl
  {
    private ObservableCollection<Tweet> _tweets = new ObservableCollection<Tweet>();

    private static string _mapKey = "### your key goes here ###";
    
    private static string _atomNamespace = "http://www.w3.org/2005/Atom";

    private static string _bingNamespace = "http://schemas.microsoft.com/search/local/ws/rest/v1";

    private static XName _pointName = XName.Get("Point", _bingNamespace);

    private static XName _latitudeName = XName.Get("Latitude", _bingNamespace);

    private static XName _longitudeName = XName.Get("Longitude", _bingNamespace);

    private static XName _entryName = XName.Get("entry", _atomNamespace);

    private static XName _idName = XName.Get("id", _atomNamespace);

    private static XName _linkName = XName.Get("link", _atomNamespace);

    private static XName _publishedName = XName.Get("published", _atomNamespace);
    
    private static XName _nameName = XName.Get("name", _atomNamespace);

    private static XName _titleName = XName.Get("title", _atomNamespace);

    private string _twitterUrl = "http://search.twitter.com/search.atom?rpp=100&since_id=";//0&q=";

    private string _geoCodeUrl = "http://dev.virtualearth.net/REST/v1/Locations/GB/"; //NE14XF?o=xml&key=" + mapKey;

    private long _lastTweetId = 0;
    
    public MainPage()
    {

      InitializeComponent();

      // set the view to the UK
      Map.SetView(new Location(54.51655, -3.22), 5.0);
      Map.Mode = new AerialMode(false);

      // a function which find tweets with an id higher than the given, that match the given search term
      Func<string, long, IObservable<string>> searchTwitter = (searchText, lastId) =>
        {
          var uri = _twitterUrl + lastId.ToString() + "&q=" + searchText;
          var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(uri));
          var twitterSearch = Observable.FromAsyncPattern<WebResponse>(request.BeginGetResponse, request.EndGetResponse);
          return twitterSearch().Select(res => WebResponseToString(res));
        };

      // a function which given a tweet, geocodes it via Bing
      Func<UKSnowTweet, IObservable<GeoCodedUKSnowTweet>> searchBing = tweet =>
      {
        var uri = _geoCodeUrl + tweet.Postcode + "?o=xml&key=" + _mapKey;
        var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(uri));
        var twitterSearch = Observable.FromAsyncPattern<WebResponse>(request.BeginGetResponse, request.EndGetResponse);
        return twitterSearch().Select(res => WebResponseToString(res))
          .Select(response => ExtractLocationFromBingGeoCode(response))
          .Select(loc => new GeoCodedUKSnowTweet(tweet)
          {
            Location= loc
          });
      };

      // the uksnow twitter / bing mashup pipeline
      Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(30))
                .SelectMany(ticks => searchTwitter("%23uksnow", _lastTweetId))
                .Select(searchResult => ParseTwitterSearch(searchResult))
                .ObserveOnDispatcher()
                .Do(tweet => AddTweets(tweet))                
                .Do(tweets => UpdateLastTweetId(tweets))
                .SelectMany(tweets => tweets)                                
                .SelectMany(tweet => ParseTweetToUKSnow(tweet))
                .SelectMany(snowTweet => searchBing(snowTweet))
                .ObserveOnDispatcher()                
                .Subscribe(geoTweet => AddSnow(geoTweet));

      // every 5 minutes remove old snow from the map
      Observable.Timer(TimeSpan.FromMinutes(5))
                .ObserveOnDispatcher()
                .Subscribe(i => RemoveOldSnowFromMap());



      SearchResult.ItemsSource = _tweets;
    }

    /// <summary>
    /// Add the given tweets to our list, enforcing a maximum of 100.
    /// </summary>
    private void AddTweets(IEnumerable<Tweet> tweets)
    {
      LoadingIndictor.Visibility = Visibility.Collapsed;
      foreach (var tweet in tweets.Reverse())
      {
        _tweets.Insert(0, tweet);

        if (_tweets.Count > 100)
        {
          _tweets.Remove(_tweets.Last());
        }
      }
    }

    /// <summary>
    /// Adds the given tweet to the map
    /// </summary>
    private void AddSnow(GeoCodedUKSnowTweet geoTweet)
    {
      var location = new Location(geoTweet.Location.X, geoTweet.Location.Y);
      int factor = geoTweet.SnowFactor;

      Image image = new Image();
      image.Tag = geoTweet;
      image.Source = new BitmapImage(new Uri("/TwitterRx;component/snow.png", UriKind.Relative));
      image.Stretch = Stretch.None;
      image.Opacity = (double)factor / 10.0;
      Map.Children.Add(image);
      
      MapLayer.SetPosition(image, location);
      MapLayer.SetPositionOrigin(image, PositionOrigin.Center);
    }

    /// <summary>
    /// Remove any snow from the map that is more than 1 hour old
    /// </summary>
    private void RemoveOldSnowFromMap()
    {
      var remove = Map.Children.OfType<Image>().Cast<Image>()
        .Where(img => (DateTime.Now - ((GeoCodedUKSnowTweet)img.Tag).Timestamp).TotalMinutes > 60)
        .ToList();

      foreach (var rem in remove)
      {
        Map.Children.Remove(rem);
      }
    }

    /// <summary>
    /// Update the recorded Id of the most recent tweet
    /// </summary>
    private void UpdateLastTweetId(IEnumerable<Tweet> tweets)
    {
      if (tweets.Any())
      {
        _lastTweetId = Math.Max(_lastTweetId, tweets.Max(t => t.Id));
      }
    }

    /// <summary>
    /// Parses the given tweet returning a UKSnowTweet if the postcode and snow
    /// regexes match
    /// </summary>
    private IEnumerable<UKSnowTweet> ParseTweetToUKSnow(Tweet tweet)
    {
      string postcode = GetFirstMatch(tweet.Title, @"[A-Za-z]{1,2}[0-9]{1,2}");
      string snowFactor = GetFirstMatch(tweet.Title, @"[0-9]{1,2}/10");
      if (postcode!="" && snowFactor!="")
      {
        yield return new UKSnowTweet(tweet)
        {
          Postcode = postcode,
          SnowFactor = int.Parse(snowFactor.Split('/')[0])
        };
      }
    }

    /// <summary>
    /// A helper method for finding the first match for the given regex
    /// </summary>
    private string GetFirstMatch(string tweet, string regex)
    {
      Regex r = new Regex(regex);
      Match m = r.Match(tweet);
      if (m.Success)
      {
        return m.Groups[0].Value;
      }
      return "";
    }

    /// <summary>
    /// Parses the response from the Bing Maps geocode service
    /// </summary>
    private Point ExtractLocationFromBingGeoCode(string response)
    {
      return XDocument.Parse(response).Descendants(_pointName)
                                      .Select(pointElement => new Point()
                                      {
                                        X = double.Parse(pointElement.Descendants(_latitudeName).Single().Value),
                                        Y = double.Parse(pointElement.Descendants(_longitudeName).Single().Value),
                                      })
                                      .FirstOrDefault();
    }

    /// <summary>
    /// Parses a Tweet search response
    /// </summary>
    private IEnumerable<Tweet> ParseTwitterSearch(string response)
    {
      var doc = XDocument.Parse(response);
      return doc.Descendants(_entryName)
                .Select(entryElement => new Tweet()
                {
                  Title = entryElement.Descendants(_titleName).Single().Value,
                  Id = long.Parse(entryElement.Descendants(_idName).Single().Value.Split(':')[2]),
                  ProfileImageUrl = entryElement.Descendants(_linkName).Skip(1).First().Attribute("href").Value,
                  Timestamp = DateTime.Parse(entryElement.Descendants(_publishedName).Single().Value),
                  Author = ParseTwitterName(entryElement.Descendants(_nameName).Single().Value)
                });
    }

    private string ParseTwitterName(string name)
    {
      int bracketLocation = name.IndexOf("(");
      return name.Substring(0, bracketLocation - 1);
    }

    private string WebResponseToString(WebResponse webResponse)
    {
      HttpWebResponse response = (HttpWebResponse)webResponse;
      using (StreamReader reader = new StreamReader(response.GetResponseStream()))
      {
        return reader.ReadToEnd();
      }
    }

    /// <summary>
    /// A tweet!
    /// </summary>
    public class Tweet
    {
      public long Id { get; set; }
      public string Title { get; set; }
      public string Author { get; set; }
      public string ProfileImageUrl { get; set; }
      public DateTime Timestamp { get; set; }

      public Tweet()
      { }

      public Tweet(Tweet tweet)
      {
        Id = tweet.Id;
        Title = tweet.Title;
        ProfileImageUrl = tweet.ProfileImageUrl;
        Author = tweet.Author;
        Timestamp = tweet.Timestamp;
      }

      public override string ToString()
      {
        return Title;
      }
    }
    
    /// <summary>
    /// A tweet with a postcode and snowfall factor
    /// </summary>
    public class UKSnowTweet : Tweet
    {
      public string Postcode { get; set;}
      public int SnowFactor { get; set;}

      public UKSnowTweet(Tweet tweet)
        : base(tweet)
      {
      }

      public UKSnowTweet(UKSnowTweet tweet)
        : this((Tweet)tweet)
      {
        Postcode = tweet.Postcode;
        SnowFactor = tweet.SnowFactor;
      }

      public override string ToString()
      {
        return Postcode + " " + SnowFactor.ToString() + " " + base.ToString();
      }
    }

    /// <summary>
    /// A geocoded tweet
    /// </summary>
    public class GeoCodedUKSnowTweet : UKSnowTweet
    {
      public Point Location { get; set; }

      public GeoCodedUKSnowTweet(UKSnowTweet tweet)
        : base(tweet)
      {
      }
    }

  }
}
