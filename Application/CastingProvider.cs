using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theater;
using System.Threading.Tasks.Dataflow;

namespace Application
{
    public class CastingProvider
    {
        public List<Actor> Actors
        {
            get
            {
                List<Actor> actors = new List<Actor>();
                Actor actor = new Actor()
                {
                    Act = "One",
                    Execution = (c, o, a, s, r, q) =>
                    {
                        a((value) =>
                        {
                            o("Operator", true, (p) =>
                            {
                                string op = p.Operator;

                                a((input) =>
                                {
                                    var val = 0;
                                    switch (op.ToString())
                                    {
                                        case "+":
                                            val = input.Input.LeftHand + input.Input.RightHand;
                                            r("Value", val);
                                            break;
                                        case "-":
                                            val = input.Input.LeftHand - input.Input.RightHand;
                                            r("Value", val);
                                            break;
                                        case "/":
                                            val = input.LeftHand.Text / input.RightHand.Text;
                                            r("Value", val);
                                            break;
                                        case "*":
                                            val = input.LeftHand.Text * input.RightHand.Text;
                                            r("Value", val);
                                            break;
                                    }
                                    c(s["5a4ecace-8b73-437b-bd09-91179d6ebe08"]);

                                }, false, "Input");
                            });
                        }, true, "Evaluate");

                        //o("LeftHand", (p) =>
                        //{
                        //    // Do Work
                        //    c(s["5a4ecace-8b73-437b-bd09-91179d6ebe06"]);
                        //});

                        ////q("WhatDoIDo", (p1) =>
                        ////{
                        ////    q("WhatDoIDoNow", (p2) =>
                        ////    {
                        ////        p2.WhateverIWant = 5;
                        ////    });
                        ////});

                        //o("RightHand", (p) =>
                        //{
                        //    // Do Work
                        //    c(s["5a4ecace-8b73-437b-bd09-91179d6ebe07"]);
                        //});
                    }
                };

                actors.Add(actor);

                return actors;
            }
        }
    }
}
