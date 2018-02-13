﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Theme.WPF
{
    class Singulum
    {

    }

    class SingulumData
    {
        public bool isNew = false;
    }

    class Backbone
    {
        public Backbone(string storage)
        {
            DataStorage = storage;
        }

        public void Initialize()
        {
            this.Status = 0x01; // 0x01 = init started
            if (tryLoad(DataStorage).isNew)
            {
                this.Status = 0xff; //data is new. nready for work
            }
            else
            {
                this.Status = 0x02; //ready for work (data loaded successfully)
            }
        } 
        public byte Status { get; internal set; } = 0x00;
        public string DataStorage { get; internal set; }
        public Singulum protocol = new Singulum();
        private SingulumData tryLoad(string path)
        {
            Thread.Sleep(5000);
            return new SingulumData();
        }
    }
}
