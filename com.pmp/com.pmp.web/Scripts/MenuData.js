var _Menu = [
  {
      "userLevel": ",2,",
      "title": "我是雇主",
      "menu": [
        {
            "title": "基本信息",
            "style": "",
            "url": "/Account/AccountPersonal",
            "sort": 1,
            "subMenu":
                [{
                    "title": "个人简介",
                    "style": "",
                    "url": "/Account/AccountDetail",
                    "sort": 1
                }, {
                    "title": "我的信息",
                    "style": "",
                    "url": "/Account/AccountPersonal"
                },
                {
                    "title": "实名认证",
                    "style": "",
                    "url": "/Account/AccountApprove",
                    "sort": 1
                }
                ]
        },
        {
            "title": "员工管理",
            "style": "",
            "url": "",
            "sort": 1,
            "subMenu": [
              {
                  "title": "员工列表",
                  "style": "",
                  "url": "/Account/UserList"
              }
            ]
        },
        {
            "title": "任务中心",
            "style": "",
            "url": "",
            "sort": 1,
            "subMenu": [
              {
                  "title": "我的任务",
                  "style": "",
                  "url": "/Task/MyList"
              }
            ]
        },
        {
            "title": "评价管理",
            "style": "",
            "url": "",
            "sort": 1,
            "subMenu": [
             {
                 "title": "我的评价",
                 "style": "",
                 "url": "/Task/Evaluation"
             },
             {
                 "title": "我的信誉",
                 "style": "",
                 "url": "/Task/REvaluation"
             }
            ]
        }
      ]
  },
  {
      "userLevel": ",21,22,",
      "title": "寻找项目",
      "menu": [
        {
            "title": "我的账号",
            "style": "",
            "url": "/Account/AccountPersonal",
            "sort": 1,
            "subMenu": [
                 {
                     "title": "个人简介",
                     "style": "",
                     "url": "/Account/AccountDetail",
                     "sort": 1
                 },
            {
                "title": "基本资料",
                "style": "",
                "url": "/Account/AccountPersonal",
                "sort": 1
            }, {
                "title": "实名认证",
                "style": "",
                "url": "/Account/AccountApprove",
                "sort": 1
            }
            ]
        },
        {
            "title": "我的任务",
            "style": "",
            "url": "/Task/MyList",
            "sort": 1,
            "subMenu": [
              {
                  "title": "我的任务",
                  "style": "",
                  "url": "/Task/MyList?state=1",
                  "sort": 1
              }
            ]
        },
        {
            "title": "评价管理",
            "style": "",
            "url": "",
            "sort": 1,
            "subMenu": [
            {
                "title": "我的评价",
                "style": "",
                "url": "/Task/Evaluation",
                "sort": 1
            }, {
                "title": "我的信誉",
                "style": "",
                "url": "/Task/REvaluation",
                "sort": 1
            }
            ]

        }
      ]
  },
{
    "userLevel": ",1,",
    "title": "超级管理员",
    "menu": [
      {
          "title": "我的信息",
          "style": "",
          "url": "/Account/AccountPersonal",
          "sort": 1,
          "subMenu": [
              {
                  "title": "个人简介",
                  "style": "",
                  "url": "/Account/AccountDetail",
              },
              {
                  "title": "基本信息",
                  "style": "",
                  "url": "/Account/AccountPersonal",
              },
          ]
      },
      {
          "title": "会员审核",
          "style": "",
          "url": "/Account/AccountAudit",
          "sort": 1,
          "subMenu": [{
              "title": "认证审核",
              "style": "",
              "url": "/Account/AccountAudit",
          }]
      },
      {
          "title": "会员管理",
          "style": "",
          "url": "/Account/AccountP_Approve",
          "sort": 1,
          "subMenu": [{
              "title": "公司管理",
              "style": "",
              "url": "/Account/CompanyUserList",
          },
          {
              "title": "个人用户",
              "style": "",
              "url": "/Account/UserList?type=1",
          }
          ]
      },
    ]
}
]
