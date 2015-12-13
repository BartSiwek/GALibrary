using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using GALibrary.Common;
using GALibrary.Evolutionary;
using GALibrary.Evolutionary.MuPlusLambda;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //SGA
            SGATester sgaTester = new SGATester();
            sgaTester.Run();
            //OnePlusOne
            //OnePlusOneTester onePlusOneTester = new OnePlusOneTester();
            //onePlusOneTester.Run();
            //MuPlusLambda
            //MuPlusLambdaTester muPlusLambdaTester = new MuPlusLambdaTester();
            //muPlusLambdaTester.Run();
            //Mu,Lambda
            //MuLambdaTester muLambdaTester = new MuLambdaTester();
            //muLambdaTester.Run();
            //ScalingTester scalingTester = new ScalingTester();
            //scalingTester.Run();
        }
    }
}
