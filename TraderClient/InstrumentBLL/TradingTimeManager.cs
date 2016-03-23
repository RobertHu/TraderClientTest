using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocal;

namespace TraderClient.InstrumentBLL
{
    internal static class TradingTimeManager
    {
        internal static void InstrumentDayCloseTest(Protocal.IGatewayService gatewayService)
        {
            DateTime dayCloseTime = DateTime.Now.AddMinutes(2);
            List<string> instruments = new List<string>
            {
                "1DFB99D4-2B76-48B0-9109-0A67265F5B9F",
                "1214720E-01DC-4E68-A271-0AFA8B5E72E1",
                "36EA5E9D-A12C-45F4-AC5C-0F9D8E12BDF7",
                "0ADF8B7D-238D-4F29-8B13-14307FDA9701",
                "33C4C6E2-E33C-4A21-A01A-35F4EC647890",
                "963163B3-3B23-4050-B33A-372BD4323828",
                "2A346B2A-BD3D-4950-8CE0-384D5C16A73E",
                "706FB632-AB1E-4EA9-B146-4633E39C161E",
                "3C4B1A43-4D50-4EF3-9FAD-521413BADAF9",
                "670590DB-8CE6-4676-AC68-7701FFC5410A",
                "C2EF11CC-E976-4A68-9D9E-8B7DDAFA89F3",
                "66ADC06C-C5FE-4428-867F-BE97650EB3B4",
                "ABC59161-5F85-488D-AD1E-CCDC0FD82D76",
                "7B7E1944-4B61-4657-8DE6-DE623430C387",
                "3D4FA404-C1ED-4B64-9390-E14B6D04610F",
                "C604B7DB-16C8-4A92-B076-EA0DC38BC84A"
            };

            List<InstrumentCheckPoint> checkPoints = new List<InstrumentCheckPoint>(instruments.Count);

            foreach (var eachInstrumentId in instruments)
            {
                checkPoints.Add(new InstrumentCheckPoint
                {
                    InstrumentId = Guid.Parse(eachInstrumentId),
                    CheckTime = dayCloseTime,
                    Status = Protocal.TradingInstrument.InstrumentStatus.DayClose
                });
            }
            gatewayService.AddCheckPointForTest(checkPoints);
        }

    }
}
