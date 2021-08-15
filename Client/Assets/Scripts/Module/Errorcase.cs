using System;
using System.Collections;
using System.Collections.Generic;

namespace Module
{
    public class Errorcase
    {
        public Dictionary<string, string> Errorcases;

        public Errorcase()
        {
            Errorcases = new Dictionary<string, string>();
            Errorcases.Add
            (
                "Inappropriate ID", "아이디 조건을 만족하지 않습니다."
            );
            Errorcases.Add
            (
                "Inappropriate PWD", "비밀번호 조건을 만족하지 않습니다."
            );
            Errorcases.Add
            (
                "Not matched PWD", "비밀번호 간에 일치하지 않습니다."
            );
            Errorcases.Add
            (
                "Duplicated ID", "이미 사용 중인 아이디 혹은 이메일 입니다."
            );
        }

        public string GetErrorcase(string errorcase)
        {
            return Errorcases[errorcase];
        }
    }
}