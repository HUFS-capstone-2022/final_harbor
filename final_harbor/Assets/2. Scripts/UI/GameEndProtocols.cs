using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEndProtocols
{
    public class Packets
    {
        public class find_req
        {
            public string userId;
            public string userName;
            public string userClearTime;
            public string userClearType;
            public find_req(string userId, string userName, string userClearTime, string userClearType)
            {
                this.userId = userId;
                this.userName = userName;
                this.userClearTime = userClearTime;
                this.userClearType = userClearType;
            }
        }

        // : 역직렬화
        // : 서버 -> 클라이언트
        public class find_res
        {
            public List<info_userData> end;
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