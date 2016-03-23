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
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Threading;
using System.Reactive.Linq;

namespace TwitterRx
{
  public partial class MainPage : UserControl
  {
    private static string _atomNamespace = "http://www.w3.org/2005/Atom";

    private static XName _entryName = XName.Get("entry", _atomNamespace);

    private static XName _titleName = XName.Get("title", _atomNamespace);

    private static XName _idName = XName.Get("id", _atomNamespace);

    private static XName _publishedName = XName.Get("published", _atomNamespace);

    private static XName _nameName = XName.Get("name", _atomNamespace);

    private static XName _linkName = XName.Get("link", _atomNamespace);

    private string _twitterUrl = "http://search.twitter.com/search.atom?rpp=20&since_id=0&q=";
    
    public MainPage()
    {
      InitializeComponent();

      Func<string, IObservable<string>> searchTwitter = searchText =>
      {
        var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(_twitterUrl + searchText));
        var twitterSearch = Observable.FromAsyncPattern<WebResponse>(request.BeginGetResponse, request.EndGetResponse);
        return twitterSearch().Select(res => WebResponseToString(res));
      };

      Observable.FromEvent<TextChangedEventArgs>(searchTextBox, "TextChanged")
                .Select(e => ((TextBox)e.Sender).Text)
                .Where(text => text.Length > 2)
                .Do(s => searchResults.Opacity = 0.5)                       // reduce list opacity when typing                              
                .Throttle(TimeSpan.FromMilliseconds(400))
                .ObserveOnDispatcher()
                .Do(s => LoadingIndicator.Visibility = Visibility.Visible)  // show the loading indicator 
                .SelectMany(txt => searchTwitter(txt))
                .Select(searchRes => ParseTwitterSearch(searchRes))
                .ObserveOnDispatcher()
                .Do(s => LoadingIndicator.Visibility = Visibility.Collapsed) // hide the loading indicator
                .Do(s => searchResults.Opacity = 1)                          // return the list opacity to one
                .Subscribe(tweets => searchResults.ItemsSource = tweets);
    }

    private string WebResponseToString(WebResponse webResponse)
    {
      HttpWebResponse response = (HttpWebResponse)webResponse;
      using (StreamReader reader = new StreamReader(response.GetResponseStream()))
      {
        return reader.ReadToEnd();
      }
    }
    
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

  }

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
    }

    public override string ToString()
    {
      return Title;
    }
  }
}
