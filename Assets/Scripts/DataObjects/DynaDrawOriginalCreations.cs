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
                "ZZZ[IW][RmmmmKJP][bbbbbuuuuuuu([(2ffTfTfTf)s])[(rrrcrrrcrrrcrrrcrrrcrrrcrrrc)]ddddddmmmmm0[mmmmmmrrrMrrr\"HELLO\"]6[rrrrrrbbbbbbbbBbbbb\"WORLD\"",
                    "","",""),                
                // Michael R. Dunlavey 
                new DynaDrawSavedItem("America",
                "0llllll[LLffffffffRRRRRffff2Pf4Pf1P]",
                    "","",""),
                new DynaDrawSavedItem("Brain",
                    "llllll(7RRRmmmmmmRRRR3mmmPbbbLLLL7mmmmmmmmm([LLLLPm]LLLLLLLLm3P)RRRRRRRR)(bbbbbbbbbbb5PbbbbLLL4RRmmmmmmmmmmmmP)(bbbbbbbbbbbbLL)(2RmPmPmPmPmPmPmPmP)",
                    "","",""),
                new DynaDrawSavedItem("Clock",
                    "llllll[4RRRRRRRRRRRRffff]Rffffff3ffffffffLLLLLLLLLLLLPfP",
                    "","",""),
                new DynaDrawSavedItem("Clocks",
                    "[RRRffff2P][bbbbbbbbRRffff3P]uffffffff[ffffffffRRRRRRRdffff4P]RRRRRdffff5P",
                    "","",""),
                new DynaDrawSavedItem("David",
                    "LL([mmmmmmmmllllllllllffffffffffffffllllllllffffffffffffffllllllllffffffffffffff])RRRR7c",
                    "","",""),
                new DynaDrawSavedItem("Ellipse",
                    "LLffffPRRRR[bPbPbPbPbPbPbPbPbP]fPfPfPfPfPfPfPfPfPfPfPfPfP",
                    "","",""),
                new DynaDrawSavedItem("Lazy",
                    "L[5LffffRRffffLLffffP][4RffffLLffffRRffffP][3LLffRRRRffLLLLffPRRRRff7P][2RRffLLLLffRRRRffPLLLLff]",
                    "","",""),
                new DynaDrawSavedItem("Legs",
                    "LLLLLL1([ffffRRRRRRRRRRRRffff]llllllllll)2c3c4c5c6c1c2c3c4c5c6c",
                    "","",""),
                new DynaDrawSavedItem("Orbits",
                    "LLL1([mmmmlllllllf[rrrrrrmmmmmRRRRRRRRRmmP]f]ll)2c3c4c5c6c7c1c2c3c4c5c",
                    "","",""),
                new DynaDrawSavedItem("Primes",
                    "rrrrrrbbbu1LLfffRRR3fffLLLLL4fffRRRRRRR5fffPLLLLLLLLLLL6fffRRRRRRRRRRRRR7fffP",
                    "","",""),
                new DynaDrawSavedItem("Seven",
                    "[1LffffP][2LLfffffP][3LLLffffffP][4LLLLfffffffP][5LLLLLffffffffP][6LLLLLLfffffffffP][7LLLLLLLffffffffffP]",
                    "","",""),
                new DynaDrawSavedItem("Spider",
                    "L[RRffffLLLffff2P][LLffffRRRffff2P]",
                    "","",""),
                new DynaDrawSavedItem("Wheel",
                    "LL4FFFFFPRRRRRRRR7[[1fffmP]rrrr[2fffmP]rrrr[3fffmP]rrrr[4fffmP]rrrr[5fffmP]rrrr[6fffmP]rrrr[mmmllllllllfffllllfffllllfffllllfffllllfffllllfff]]",
                    "","",""),
                new DynaDrawSavedItem("Christmas",
                    "rrrrrr[llllllmmmmmmmmmmm1([bbfffPfP]LLL)2c3c4c5c6c7c][mmm1Pbbbbb2Plllmm3Pmmmllllllllm4Plllmmm5Prrrrrrrrrrmmm6Pllllmm7Prrrrrrrmmm1Prrrrrrmmmmmmmmmm2Prrrrmm3Prrrrrrmm4Pllllmmm5Prrmmm6Pllllllllmmm7Pllllmmm1Prrrrrrrrrrmmmmmm2Pllllllllmm3Plllllmmmm4Prrmmmmmmm5Prrrrrrrrmmm6Prrrrrrmmmm7P]rrrrrrmmmmmmmmmmmmmrrrrrr3frrrrrrfffllllllffffffffrrrrrrrrffffffllllllllfrrrrrrrrffffffllllllllfrrrrrrrrffffffllllllllfrrrrrrrrffffffrrrrrrrrffffffrrrrrrrrfllllllllffffffrrrrrrrrfllllllllffffffrrrrrrrrfllllllllffffffrrrrrrrrffffffffllllllfffrrrrrrf",
                    "","",""),
                // Scott Huskey Section
        		new DynaDrawSavedItem("Arms",
                    "EL1([fffffFffffRRRRRRRRRRRRPRfFfffPI]rr)2c3c4c5c6c1c2c3c4c5c6c",
                    "","",""),
                new DynaDrawSavedItem("Boomerang",
                    "5LmmmmmmmmmmmRRRRRR(rrrrrrbbbllllllffffrrrPfrrrfrrrfrrrfffflllflllffffrrrfrrrfrrrfrrPrffffrfrrffrfrfrrrrrrrrmmm)LLLLLLLLL[4c]",
                    "","",""),
                new DynaDrawSavedItem("Chaos",
                    "ffffrrrrffRfLfrPrrrrrrrrffffbbbbrrrrrrffff(rrfL)ccccccccccc(llfR)cccccccccccffff(lllllfLf)cccc7P",
                    "","",""),
                new DynaDrawSavedItem("Hypmotize",
                    "PLLLLLLL(r[frfffffRRRRRrfffffrffffLLLfffffffr])1c2c3c4c5c6c7c1c2c3c4c5c6c7c1c2c3c4c5c6c7c1c2c",
                    "","",""),
                new DynaDrawSavedItem("Morph",
                    "[R(frf)c[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc]]]]]]]]]]]][Lcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc[LLcc[RRcc]]]]]]]]]]]]",
                    "","",""),
                new DynaDrawSavedItem("Orbit II",
                    "R1[mmRmmLmmlllfLblllffrrrrrrrPrrffrrrrrrrff]rrrrrrrrr2[mmRmmLmmmRfrrrrrrrrrffrrrrrrffrrrrrrffPrrrrrrff]R4[lllllmmmRmmmLmmmmrrmRbbllllPlfrPrfrPrfrPrfrPrfrPrfrPrfrPrfrPrfrPrfrPrfrPrf]",
                    "","",""),
                new DynaDrawSavedItem("Rainbow",
                    "bbbL[R1CxxxxLxxxxxfLfxxxxLxxxxRRmmmmPmPmPmPmP]",
                    "","",""),
                new DynaDrawSavedItem("Slinky",
                    "LLLLLffff1L(flflflflflflflflflflflflflflflflflflflflflflflf)L2cL3cL4cL5cL6cL7cL1cL2cL3cL4cL5cL6cL7cL1cL2cL3cL4cL5cL6cL7cL1cL2cL3cL4cL5cL6cL7c",
                    "","",""),
                new DynaDrawSavedItem("Spinner",
                    "L1([fffffFffffRRRRRRRRRRRRPRfFfffP]rr)2c3c4c5c6c1c2c3c4c5c6c",
                    "","",""),
                new DynaDrawSavedItem("Two Wheels",
                    "BBBBBBBBBBBB[[RRmmmmR(ffffrrrrrrrffrrffbbrrrrrffffrrrrrr)1c2c3c4c5c][L(ffffrrrrrrrPffrrffbbrrrrrffffrrrrrr)1c2c3c4c5c]]",
                    "","","")
            };
        }
    }
}
