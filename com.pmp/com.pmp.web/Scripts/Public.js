Public =
    {

        /**
           * 判断是否为手机号码格式
           * 
           * @return bool
           */
        IsMobile: function (str) {
            if (str.length != 11) return false;
            var reg0 = /^13\d{5,9}$/;
            var reg1 = /^15\d{5,9}$/;
            var reg2 = /^18\d{5,9}$/;
            var reg3 = /^17\d{5,9}$/;
            var my = false;
            if (reg0.test(str)) my = true;
            if (reg1.test(str)) my = true;
            if (reg2.test(str)) my = true;
            if (reg3.test(str)) my = true;
            return my;
        }
    }