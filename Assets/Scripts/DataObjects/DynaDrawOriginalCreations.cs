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
                new DynaDrawSavedItem("SDH Hello World",  // Startup Art
                "3Z[I<World>][R4m<Point><Rocket><Smoke>][5b6u([(<Color(2)>ff<Tilter>f<Tilter>f<Tilter>f)<Shoot>])[(3rc3rc3rc3rc3rc3rc3rc)]6d5m<Color(0)>[6m3rM3r\"HELLO\"]<Color(6)>[6r8bB4b\"WORLD\"",
                    "","",""),                
                // Michael R. Dunlavey 
                new DynaDrawSavedItem("MRD America",
                "<Color(0)>llllll[LLffffffffRRRRRffff<Color(2)><Smoke>f<Color(4)><Smoke>f<Color(1)><Smoke>]",
                    "","",""),
                new DynaDrawSavedItem("MRD Brain",
                    "llllll(<Color(7)>RRRmmmmmmRRRR<Color(3)>mmm<Smoke>bbbLLLL<Color(7)>mmmmmmmmm([LLLL<Smoke>m]LLLLLLLLm<Color(3)><Smoke>)RRRRRRRR)(bbbbbbbbbbb<Color(5)><Smoke>bbbbLLL<Color(4)>RRmmmmmmmmmmmm<Smoke>)(bbbbbbbbbbbbLL)(<Color(2)>Rm<Smoke>m<Smoke>m<Smoke>m<Smoke>m<Smoke>m<Smoke>m<Smoke>m<Smoke>)",
                    "","",""),
                new DynaDrawSavedItem("MRD Clock",
                    "llllll[<Color(4)>RRRRRRRRRRRRffff]Rffffff<Color(3)>ffffffffLLLLLLLLLLLL<Smoke>f<Smoke>",
                    "","",""),
                new DynaDrawSavedItem("MRD Clocks",
                    "[RRRffff<Color(2)><Smoke>][bbbbbbbbRRffff<Color(3)><Smoke>]uffffffff[ffffffffRRRRRRRdffff<Color(4)><Smoke>]RRRRRdffff<Color(5)><Smoke>",
                    "","",""),
                new DynaDrawSavedItem("MRD David",
                    "LL([mmmmmmmmllllllllllffffffffffffffllllllllffffffffffffffllllllllffffffffffffff])RRRR<Color(7)>c",
                    "","",""),
                new DynaDrawSavedItem("MRD Ellipse",
                    "LLffff<Smoke>RRRR[b<Smoke>b<Smoke>b<Smoke>b<Smoke>b<Smoke>b<Smoke>b<Smoke>b<Smoke>b<Smoke>]f<Smoke>f<Smoke>f<Smoke>f<Smoke>f<Smoke>f<Smoke>f<Smoke>f<Smoke>f<Smoke>f<Smoke>f<Smoke>f<Smoke>f<Smoke>",
                    "","",""),
                new DynaDrawSavedItem("MRD Lazy",
                    "L[<Color(5)>LffffRRffffLLffff<Smoke>][<Color(4)>RffffLLffffRRffff<Smoke>][<Color(3)>LLffRRRRffLLLLff<Smoke>RRRRff<Color(7)><Smoke>][<Color(2)>RRffLLLLffRRRRff<Smoke>LLLLff]",
                    "","",""),
                new DynaDrawSavedItem("MRD Legs",
                    "LLLLLL<Color(1)>([ffffRRRRRRRRRRRRffff]llllllllll)<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c",
                    "","",""),
                new DynaDrawSavedItem("MRD Orbits",
                    "LLL<Color(1)>([mmmmlllllllf[rrrrrrmmmmmRRRRRRRRRmm<Smoke>]f]ll)<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(7)>c<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c",
                    "","",""),
                new DynaDrawSavedItem("MRD Primes",
                    "rrrrrrbbbu<Color(1)>LLfffRRR<Color(3)>fffLLLLL<Color(4)>fffRRRRRRR<Color(5)>fff<Smoke>LLLLLLLLLLL<Color(6)>fffRRRRRRRRRRRRR<Color(7)>fff<Smoke>",
                    "","",""),
                new DynaDrawSavedItem("MRD Seven",
                    "[<Color(1)>Lffff<Smoke>][<Color(2)>LLfffff<Smoke>][<Color(3)>LLLffffff<Smoke>][<Color(4)>LLLLfffffff<Smoke>][<Color(5)>LLLLLffffffff<Smoke>][<Color(6)>LLLLLLfffffffff<Smoke>][<Color(7)>LLLLLLLffffffffff<Smoke>]",
                    "","",""),
                new DynaDrawSavedItem("MRD Spider",
                    "L[RRffffLLLffff<Color(2)><Smoke>][LLffffRRRffff<Color(2)><Smoke>]",
                    "","",""),
                new DynaDrawSavedItem("MRD Wheel",
                    "LL<Color(4)>FFFFF<Smoke>RRRRRRRR<Color(7)>[[<Color(1)>fffm<Smoke>]rrrr[<Color(2)>fffm<Smoke>]rrrr[<Color(3)>fffm<Smoke>]rrrr[<Color(4)>fffm<Smoke>]rrrr[<Color(5)>fffm<Smoke>]rrrr[<Color(6)>fffm<Smoke>]rrrr[mmmllllllllfffllllfffllllfffllllfffllllfffllllfff]]",
                    "","",""),
                new DynaDrawSavedItem("MRD Christmas",
                    "rrrrrr[llllllmmmmmmmmmmm<Color(1)>([bbfff<Smoke>f<Smoke>]LLL)<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(7)>c][mmm<Color(1)><Smoke>bbbbb<Color(2)><Smoke>lllmm<Color(3)><Smoke>mmmllllllllm<Color(4)><Smoke>lllmmm<Color(5)><Smoke>rrrrrrrrrrmmm<Color(6)><Smoke>llllmm<Color(7)><Smoke>rrrrrrrmmm<Color(1)><Smoke>rrrrrrmmmmmmmmmm<Color(2)><Smoke>rrrrmm<Color(3)><Smoke>rrrrrrmm<Color(4)><Smoke>llllmmm<Color(5)><Smoke>rrmmm<Color(6)><Smoke>llllllllmmm<Color(7)><Smoke>llllmmm<Color(1)><Smoke>rrrrrrrrrrmmmmmm<Color(2)><Smoke>llllllllmm<Color(3)><Smoke>lllllmmmm<Color(4)><Smoke>rrmmmmmmm<Color(5)><Smoke>rrrrrrrrmmm<Color(6)><Smoke>rrrrrrmmmm<Color(7)><Smoke>]rrrrrrmmmmmmmmmmmmmrrrrrr<Color(3)>frrrrrrfffllllllffffffffrrrrrrrrffffffllllllllfrrrrrrrrffffffllllllllfrrrrrrrrffffffllllllllfrrrrrrrrffffffrrrrrrrrffffffrrrrrrrrfllllllllffffffrrrrrrrrfllllllllffffffrrrrrrrrfllllllllffffffrrrrrrrrffffffffllllllfffrrrrrrf",
                    "","",""),
                // Scott Huskey Section
        		new DynaDrawSavedItem("SDH Arms",
                    "EL<Color(1)>([fffffFffffRRRRRRRRRRRR<Smoke>RfFfff<Smoke>I]rr)<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c",
                    "","",""),
                new DynaDrawSavedItem("SDH Boomerang",
                    "<Color(5)>LmmmmmmmmmmmRRRRRR(rrrrrrbbbllllllffffrrr<Smoke>frrrfrrrfrrrfffflllflllffffrrrfrrrfrrrfrr<Smoke>rffffrfrrffrfrfrrrrrrrrmmm)LLLLLLLLL[<Color(4)>c]",
                    "","",""),
                new DynaDrawSavedItem("SDH Chaos",
                    "ffffrrrrffRfLfr<Smoke>rrrrrrrrffffbbbbrrrrrrffff(rrfL)ccccccccccc(llfR)cccccccccccffff(lllllfLf)cccc<Color(7)><Smoke>",
                    "","",""),
                new DynaDrawSavedItem("SDH Hypmotize",
                    "<Smoke>LLLLLLL(r[frfffffRRRRRrfffffrffffLLLfffffffr])<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(7)>c<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(7)>c<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(7)>c<Color(1)>c<Color(2)>c",
                    "","",""),
                new DynaDrawSavedItem("SDH Morph",
                    "[R(frf)c[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc]]]]]]]]]]]][Lcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc]]]]]]]]]]]]",
                    "","",""),
                new DynaDrawSavedItem("SDH Orbit II",
                    "R<Color(1)>[mmRmmLmmlllfLblllffrrrrrrr<Smoke>rrffrrrrrrrff]rrrrrrrrr<Color(2)>[mmRmmLmmmRfrrrrrrrrrffrrrrrrffrrrrrrff<Smoke>rrrrrrff]R<Color(4)>[lllllmmmRmmmLmmmmrrmRbbllll<Smoke>lfr<Smoke>rfr<Smoke>rfr<Smoke>rfr<Smoke>rfr<Smoke>rfr<Smoke>rfr<Smoke>rfr<Smoke>rfr<Smoke>rfr<Smoke>rfr<Smoke>rf]",
                    "","",""),
                new DynaDrawSavedItem("SDH Rainbow",
                    "bbbL[R<Color(1)>CxxxxLxxxxxfLfxxxxLxxxxRRmmmm<Smoke>m<Smoke>m<Smoke>m<Smoke>m<Smoke>]",
                    "","",""),
                new DynaDrawSavedItem("SDH Slinky",
                    "LLLLLffff<Color(1)>L(flflflflflflflflflflflflflflflflflflflflflflflf)L<Color(2)>cL<Color(3)>cL<Color(4)>cL<Color(5)>cL<Color(6)>cL<Color(7)>cL<Color(1)>cL<Color(2)>cL<Color(3)>cL<Color(4)>cL<Color(5)>cL<Color(6)>cL<Color(7)>cL<Color(1)>cL<Color(2)>cL<Color(3)>cL<Color(4)>cL<Color(5)>cL<Color(6)>cL<Color(7)>cL<Color(1)>cL<Color(2)>cL<Color(3)>cL<Color(4)>cL<Color(5)>cL<Color(6)>cL<Color(7)>c",
                    "","",""),
                new DynaDrawSavedItem("SDH Spinner",
                    "L<Color(1)>([fffffFffffRRRRRRRRRRRR<Smoke>RfFfff<Smoke>]rr)<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c<Color(6)>c",
                    "","",""),
                new DynaDrawSavedItem("SDH Two Wheels",
                    "BBBBBBBBBBBB[[RRmmmmR(ffffrrrrrrrffrrffbbrrrrrffffrrrrrr)<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c][L(ffffrrrrrrr<Smoke>ffrrffbbrrrrrffffrrrrrr)<Color(1)>c<Color(2)>c<Color(3)>c<Color(4)>c<Color(5)>c]]",
                    "","","")
            };
        }
    }
}
