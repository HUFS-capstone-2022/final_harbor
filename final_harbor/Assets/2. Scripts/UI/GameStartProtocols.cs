using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStartProtocols
{
    public class Packets
    {
        public class find_req
        {
            public string userId;
            public find_req(string userId)
            {
                this.userId = userId;
            }
        }

        // : 역직렬화
        // : 서버 -> 클라이언트
        public class find_res
        {
            public List<info_userData> True;
            public List<info_userData> Normal;
            public List<info_userData> Bad;
        }

        // : 역직렬화 대상
        public class info_userData
        {
            public int id;
            public string name;
            public string clear_time;
        }
    }
}