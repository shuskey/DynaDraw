using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DataObjects
{
    [System.Serializable]
    public class DynaDrawOriginalCreations
    {        
        public string OriginalComments { get; set; }
        public List<DynaDrawSavedItem> OriginalCreationsList { get; set; }

        public List<string> JustTitles()
        {
            var returnThisList = new List<string>();
            foreach (var originalCreationItem in OriginalCreationsList)
            {
                returnThisList.Add(originalCreationItem.Title);
            }
            return returnThisList;
        }

        public DynaDrawOriginalCreations()
        {
            OriginalComments = "Creations from the original Michael R. Dunlavey DynaDraw and Scott Huskey additions.";
            OriginalCreationsList = new List<DynaDrawSavedItem>()
            {
                new DynaDrawSavedItem("Choose from the below Masterpieces:",  // Startup Art
                "3Z[I<World>][R4m<Point><Rocket><Smoke>][5b6u([(<Color(2)>ff<Tilter>f<Tilter>f<Tilter>f)<Shoot>])[(3rc3rc3rc3rc3rc3rc3rc)]6d5m<Color(0)>[6m3rM3r\"HELLO\"]<Color(6)>[6r8bB4b\"WORLD\"",
                    "","",""),                
                // Michael R. Dunlavey 
                new DynaDrawSavedItem("America",
                "<Color(0)>llllll[LLffffffffRRRRRffff<Color(2)>Pf<Color(4)>Pf<Color(1)>P]",
                    "","",""),
                new DynaDrawSavedItem("Brain",
                    "llllll(<Color(7)>RRRmmmmmmRRRR<Color(3)>mmmPbbbLLLL<Color(7)>mmmmmmmmm([LLLLPm]LLLLLLLLm<Color(3)>P)RRRRRRRR)(bbbbbbbbbbb<Color(5)>PbbbbLLL<Color(4)>RRmmmmmmmmmmmmP)(bbbbbbbbbbbbLL)(<Color(2)>RmPmPmPmPmPmPmPmP)",
                    "","",""),
                new DynaDrawSavedItem("Clock",
                    "llllll[<Color(4)>RRRRRRRRRRRRffff]Rffffff<Color(3)>ffffffffLLLLLLLLLLLLPfP",
                    "","",""),
                new DynaDrawSavedItem("Clocks",
                    "[RRRffff<Color(2)>P][bbbbbbbbRRffff<Color(3)>P]uffffffff[ffffffffRRRRRRRdffff<Color(4)>P]RRRRRdffff<Color(5)>P",
                    "","",""),
                new DynaDrawSavedItem("David",
                    "LL([mmmmmmmmllllllllllffffffffffffffllllllllffffffffffffffllllllllffffffffffffff])RRRR<Color(7)>c",
                    "","",""),
                new DynaDrawSavedItem("Ellipse",
                    "LLffffPRRRR[bPbPbPbPbPbPbPbPbP]fPfPfPfPfPfPfPfPfPfPfPfPfP",
                    "","",""),
                new DynaDrawSavedItem("Lazy",
                    "L[<Color(5)>LffffRRffffLLffffP][<Color(4)>RffffLLffffRRffffP][<Color(3)>LLffRRRRffLLLLffPRRRRff<Color(7)>P][<Color(2)>RRffLLLLffRRRRffPLLLLff]",
                    "","",""),
                new DynaDrawSavedItem("Legs",
                    "LLLLLL<Color(1)>([ffffRRRRRRRRRRRRffff]llllllllll)<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c",
                    "","",""),
                new DynaDrawSavedItem("Orbits",
                    "LLL<Color(1)>([mmmmlllllllf[rrrrrrmmmmmRRRRRRRRRmmP]f]ll)<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(7)>c<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c",
                    "","",""),
                new DynaDrawSavedItem("Primes",
                    "rrrrrrbbbu<Color(1)>LLfffRRR<Color(3)>fffLLLLL<Color(4)>fffRRRRRRR<Color(5)>fffPLLLLLLLLLLL<Color(6)>fffRRRRRRRRRRRRR<Color(7)>fffP",
                    "","",""),
                new DynaDrawSavedItem("Seven",
                    "[<Color(1)>LffffP][<Color(2)>LLfffffP][<Color(3)>LLLffffffP][<Color(4)>LLLLfffffffP][<Color(5)>LLLLLffffffffP][<Color(6)>LLLLLLfffffffffP][<Color(7)>LLLLLLLffffffffffP]",
                    "","",""),
                new DynaDrawSavedItem("Spider",
                    "L[RRffffLLLffff<Color(2)>P][LLffffRRRffff<Color(2)>P]",
                    "","",""),
                new DynaDrawSavedItem("Wheel",
                    "LL<Color(4)>FFFFFPRRRRRRRR<Color(7)>[[<Color(1)>fffmP]rrrr[<Color(2)>fffmP]rrrr[<Color(3)>fffmP]rrrr[<Color(4)>fffmP]rrrr[<Color(5)>fffmP]rrrr[<Color(6)>fffmP]rrrr[mmmllllllllfffllllfffllllfffllllfffllllfffllllfff]]",
                    "","",""),
                new DynaDrawSavedItem("Christmas",
                    "rrrrrr[llllllmmmmmmmmmmm<Color(1)>([bbfffPfP]LLL)<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(7)>c][mmm<Color(1)>Pbbbbb<Color(2)>Plllmm<Color(3)>Pmmmllllllllm<Color(4)>Plllmmm<Color(5)>Prrrrrrrrrrmmm<Color(6)>Pllllmm<Color(7)>Prrrrrrrmmm<Color(1)>Prrrrrrmmmmmmmmmm<Color(2)>Prrrrmm<Color(3)>Prrrrrrmm<Color(4)>Pllllmmm<Color(5)>Prrmmm<Color(6)>Pllllllllmmm<Color(7)>Pllllmmm<Color(1)>Prrrrrrrrrrmmmmmm<Color(2)>Pllllllllmm<Color(3)>Plllllmmmm<Color(4)>Prrmmmmmmm<Color(5)>Prrrrrrrrmmm<Color(6)>Prrrrrrmmmm<Color(7)>P]rrrrrrmmmmmmmmmmmmmrrrrrr<Color(3)>frrrrrrfffllllllffffffffrrrrrrrrffffffllllllllfrrrrrrrrffffffllllllllfrrrrrrrrffffffllllllllfrrrrrrrrffffffrrrrrrrrffffffrrrrrrrrfllllllllffffffrrrrrrrrfllllllllffffffrrrrrrrrfllllllllffffffrrrrrrrrffffffffllllllfffrrrrrrf",
                    "","",""),
                // Scott Huskey Section
        		new DynaDrawSavedItem("Arms",
                    "EL<Color(1)>([fffffFffffRRRRRRRRRRRRPRfFfffPI]rr)<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c",
                    "","",""),
                new DynaDrawSavedItem("Boomerang",
                    "<Color(5)>LmmmmmmmmmmmRRRRRR(rrrrrrbbbllllllffffrrrPfrrrfrrrfrrrfffflllflllffffrrrfrrrfrrrfrrPrffffrfrrffrfrfrrrrrrrrmmm)LLLLLLLLL[<Color(4)>c]",
                    "","",""),
                new DynaDrawSavedItem("Chaos",
                    "ffffrrrrffRfLfrPrrrrrrrrffffbbbbrrrrrrffff(rrfL)ccccccccccc(llfR)cccccccccccffff(lllllfLf)cccc<Color(7)>P",
                    "","",""),
                new DynaDrawSavedItem("Hypmotize",
                    "PLLLLLLL(r[frfffffRRRRRrfffffrffffLLLfffffffr])<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(7)>c<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(7)>c<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(7)>c<Color(1)>c<Color(2)>c",
                    "","",""),
                new DynaDrawSavedItem("Morph",
                    "[R(frf)c[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc]]]]]]]]]]]][Lcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc]]]]]]]]]]]]",
                    "","",""),
                new DynaDrawSavedItem("Orbit II",
                    "R<Color(1)>[mmRmmLmmlllfLblllffrrrrrrrPrrffrrrrrrrff]rrrrrrrrr<Color(2)>[mmRmmLmmmRfrrrrrrrrrffrrrrrrffrrrrrrffPrrrrrrff]R<Color(4)>[lllllmmmRmmmLmmmmrrmRbbllllPlfrPrfrPrfrPrfrPrfrPrfrPrfrPrfrPrfrPrfrPrfrPrf]",
                    "","",""),
                new DynaDrawSavedItem("Rainbow",
                    "bbbL[R<Color(1)>CxxxxLxxxxxfLfxxxxLxxxxRRmmmmPmPmPmPmP]",
                    "","",""),
                new DynaDrawSavedItem("Slinky",
                    "LLLLLffff<Color(1)>L(flflflflflflflflflflflflflflflflflflflflflflflf)L<Color(2)>cL<Color(3)>cL<Color(4)>cL<Color(5)>cL<Color(6)>cL<Color(7)>cL<Color(1)>cL<Color(2)>cL<Color(3)>cL<Color(4)>cL<Color(5)>cL<Color(6)>cL<Color(7)>cL<Color(1)>cL<Color(2)>cL<Color(3)>cL<Color(4)>cL<Color(5)>cL<Color(6)>cL<Color(7)>cL<Color(1)>cL<Color(2)>cL<Color(3)>cL<Color(4)>cL<Color(5)>cL<Color(6)>cL<Color(7)>c",
                    "","",""),
                new DynaDrawSavedItem("Spinner",
                    "L<Color(1)>([fffffFffffRRRRRRRRRRRRPRfFfffP]rr)<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c",
                    "","",""),
                new DynaDrawSavedItem("Two Wheels",
                    "BBBBBBBBBBBB[[RRmmmmR(ffffrrrrrrrffrrffbbrrrrrffffrrrrrr)<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c][L(ffffrrrrrrrPffrrffbbrrrrrffffrrrrrr)<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c]]",
                    "","","")
            };
        }
    }
}
