using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iExchange.Common;
using log4net;

namespace TraderClient.QuotationBll
{
    internal sealed class QuotationSender
    {
        internal static readonly QuotationSender Default = new QuotationSender();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(QuotationSender));

        private Dictionary<string, List<string>> _quotePoliciesPerInstrumentDict;

        private Protocal.IGatewayService _gatewayClient;

        static QuotationSender() { }

        private QuotationSender()
        {
            _quotePoliciesPerInstrumentDict = new Dictionary<string, List<string>>
            {
                {"2eab5b94-1a8e-4604-ac38-6f4453a860a8".ToLower(), new List<string>{"9CA623E7-1E7D-4B3D-BC84-47B92059B26E","AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}},
                {"33C4C6E2-E33C-4A21-A01A-35F4EC647890".ToLower(), new List<string>{"9CA623E7-1E7D-4B3D-BC84-47B92059B26E", "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}},
                {"0ADF8B7D-238D-4F29-8B13-14307FDA9701".ToLower(), new List<string>{"AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}},
                {"2E42C798-97E7-4702-AFBA-0E6ABA0575D6".ToLower(), new List<string>{"AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}},
                {"B222346F-6A3D-461B-90FA-14728672C4B6".ToLower(),new List<string>{"AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}},
                {"706fb632-ab1e-4ea9-b146-4633e39c161e".ToLower(), new List<string>{"AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}},
                {"66ADC06C-C5FE-4428-867F-BE97650EB3B4".ToLower(),new List<string>{"AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}},
                {"670590DB-8CE6-4676-AC68-7701FFC5410A".ToLower(),new List<string>{"AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2", "9CA623E7-1E7D-4B3D-BC84-47B92059B26E"}},
                {"36EA5E9D-A12C-45F4-AC5C-0F9D8E12BDF7".ToLower(),new List<string>{"AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"} },
                {"3C4B1A43-4D50-4EF3-9FAD-521413BADAF9".ToLower(),new List<string>{"AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}},
                {"1DFB99D4-2B76-48B0-9109-0A67265F5B9F".ToLower(), new List<string>{"AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}},
                {"1214720E-01DC-4E68-A271-0AFA8B5E72E1".ToLower(), new List<string>{"AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}},
                {"3D4FA404-C1ED-4B64-9390-E14B6D04610F".ToLower(), new List<string>{"9CA623E7-1E7D-4B3D-BC84-47B92059B26E", "5B691D3C-319D-4402-9908-5AA2BD2CD429", "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}},
                {"B2EF248D-4EEB-465C-B178-348411C79C41".ToLower(),new List<string>{"9CA623E7-1E7D-4B3D-BC84-47B92059B26E", "5B691D3C-319D-4402-9908-5AA2BD2CD429", "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}}

            };
        }

        internal void Initialize(Protocal.IGatewayService gatewayClient)
        {
            _gatewayClient = gatewayClient;
        }

        internal void SendQuotation(string instrumentId, string ask, string bid)
        {
            List<string> quotePolicies;
            if (!_quotePoliciesPerInstrumentDict.TryGetValue(instrumentId.ToLower(), out quotePolicies))
            {
                Logger.WarnFormat("can't find instrumentId= {0}", instrumentId);
                return;
            }
            var quotations = new List<OverridedQuotation>();
            foreach (var eachQuotePolicy in quotePolicies)
            {
                var quotation = this.CreateQuotation(Guid.Parse(instrumentId), Guid.Parse(eachQuotePolicy), ask, bid);
                quotations.Add(quotation);
            }
            _gatewayClient.SetQuotation(quotations.ToArray());
        }

        private OverridedQuotation CreateQuotation(Guid instrumentId, Guid quotePolicyId, string ask, string bid)
        {
            OverridedQuotation result = new OverridedQuotation();
            result.InstrumentID = instrumentId;
            result.QuotePolicyID = quotePolicyId;
            result.Timestamp = DateTime.Now;
            result.Ask = ask;
            result.Bid = bid;
            return result;
        }


    }
}
