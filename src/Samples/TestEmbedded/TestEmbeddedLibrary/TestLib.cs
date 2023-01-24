﻿using System;
using Alpha;
using Beta;
using Gamma;
using Windows.Media;

namespace TestEmbeddedLibrary
{
    class MyAlpha : IAlpha
    {
        public int Five() { return 5; }
    }

    class MyBeta : IBeta
    {
        public int CallFive(Alpha.IAlpha a) { return a.Five(); }
    }

    class MyGreek : IAlpha, IGamma
    {
        public int Five() { return 5; }
        public int Six() { return 6; }
    }

    public class TestLib 
    {
        public TestLib()
        {
        }

        internal int Test1_Helper(IAlpha alpha) { return alpha.Five(); }

        public int Test1() 
        {
            MyAlpha a = new();
            MyGreek g = new();
            return Test1_Helper(a) + Test1_Helper(g);
        }

        public int Test2()
        {
            MyGreek g = new();
            MyBeta b = new();
            return b.CallFive(g);
        }

        public (bool, bool) Test3()
        {
            IAlpha myAlpha = new MyAlpha();
            MyGreek myGreek = new();
            QIAgent qiAgent = new();
            return (qiAgent.CheckForIGamma(myAlpha), qiAgent.CheckForIGamma(myGreek));
        }

        internal QIAgent GetQIAgent() { return new QIAgent(); }

        public int Test4()
        {
            QIAgent qiAgent = GetQIAgent();
            var x = qiAgent.IdentityAlpha(new MyAlpha());
            return qiAgent.Run(x);
        }

        public int Test5()
        {
            // make a Windows.Media.AudioFrame   
            var aframe = new Windows.Media.AudioFrame(20);
            using (AudioBuffer abuff = aframe.LockBuffer(AudioBufferAccessMode.Read))
            {
                return (int)abuff.Capacity;
            }
        }
    }

}
