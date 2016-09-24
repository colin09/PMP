using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.model.response
{
    class MessageResponse
    {
    }


    public class MsgRes
    {
        public MsgRes() { }
        public MsgRes(int state, string msg)
        {
            State = state;
            Message = msg;
        }


        public int State { set; get; }
        public string Mehod { set; get; }
        public string Message { set; get; }
        public string Result { set; get; }
    }
}
