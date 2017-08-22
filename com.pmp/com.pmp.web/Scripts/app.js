var taskApp = angular.module('taskApp', []);
var userApp = angular.module('userApp', []);

userApp.filter('frmDate', function () {
    return function frmDate(date) {
        console.log(date);
        if (date == undefined)
            return;
        var formatDate = eval('new ' + date.substr(1, date.length - 2));

        var month = (formatDate.getMonth() + 1) < 10 ? ("0" + (formatDate.getMonth() + 1)) : (formatDate.getMonth() + 1);
        var day = formatDate.getDate() < 10 ? ("0" + formatDate.getDate()) : formatDate.getDate();
        var hour = formatDate.getHours() < 10 ? ("0" + formatDate.getHours()) : formatDate.getHours();
        var minute = formatDate.getMinutes() < 10 ? ("0" + formatDate.getMinutes()) : formatDate.getMinutes();
        var second = formatDate.getSeconds() < 10 ? ("0" + formatDate.getSeconds()) : formatDate.getSeconds();

        return formatDate.getFullYear() + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
    }
});


userApp.service('DataFormat', function () {
    this.longDate = function (formatDate) {

        var month = (formatDate.getMonth() + 1) < 10 ? ("0" + (formatDate.getMonth() + 1)) : (formatDate.getMonth() + 1);
        var day = formatDate.getDate() < 10 ? ("0" + formatDate.getDate()) : formatDate.getDate();

        return formatDate.getFullYear() + "-" + month + "-" + day ;
    }
    this.longTimeNow = function () {
        var formatDate = new Date();
        var month = (formatDate.getMonth() + 1) < 10 ? ("0" + (formatDate.getMonth() + 1)) : (formatDate.getMonth() + 1);
        var day = formatDate.getDate() < 10 ? ("0" + formatDate.getDate()) : formatDate.getDate();
        var hour = formatDate.getHours() < 10 ? ("0" + formatDate.getHours()) : formatDate.getHours();
        var minute = formatDate.getMinutes() < 10 ? ("0" + formatDate.getMinutes()) : formatDate.getMinutes();
        var second = formatDate.getSeconds() < 10 ? ("0" + formatDate.getSeconds()) : formatDate.getSeconds();

        return formatDate.getFullYear() + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
    }

    this.dayLater = function (late) {
        var formatDate = new Date();

        var oneDay = 24 * 60 * 60 * 1000;
        var later = oneDay * late;
        formatDate.setTime(formatDate.getTime() + later);

        var day = formatDate.getDate() < 10 ? ("0" + formatDate.getDate()) : formatDate.getDate();
        var month = (formatDate.getMonth() + 1) < 10 ? ("0" + (formatDate.getMonth() + 1)) : (formatDate.getMonth() + 1);

        return formatDate.getFullYear() + "-" + month + "-" + day;
    }
    this.monthLater = function (late) {
        late += 1;
        var formatDate = new Date();

        var carryYear = 0;
        var month = (formatDate.getMonth() + late) < 10 ? ("0" + (formatDate.getMonth() + late)) : (formatDate.getMonth() + late);
        if (formatDate.getMonth() + late > 12) {
            month = (formatDate.getMonth() + late - 12) < 10 ? ("0" + (formatDate.getMonth() + late - 12)) : (formatDate.getMonth() + late - 12);
            carryYear = 1;
        }
        var day = formatDate.getDate() < 10 ? ("0" + formatDate.getDate()) : formatDate.getDate();

        return (formatDate.getFullYear() + carryYear) + "-" + month + "-" + day;
    }

    //最多允许2位小数
    this.toDecimal2 = function (fmtVal, defaultVal) {
        var flag = parseFloat(fmtVal);
        if (isNaN(flag)) {
            return defaultVal;
        }
        var pointIndex = fmtVal.toString().indexOf('.');
        if (pointIndex >= 0) {
            if (fmtVal.toString().length > pointIndex + 3) {
                fmtVal = fmtVal.substr(0, pointIndex + 3);
                return parseFloat(fmtVal).toFixed(2);
            }
        }
        if (isNaN(fmtVal))
            return parseFloat(fmtVal);
        return fmtVal;
    }
});



userApp.controller("UserTaskDetailController", function ($scope, $http, $filter) {
    $scope.showProcess = false;
    var id = $("#projectId").val();
    $http.get("/Task/GetTaskProcess?id=" + id).success(function (response) {
        if (response.error) {
            console.log(response.error);
        } else {
            $scope.showProcess = true;
            $scope.processList = response;

            $scope.totalServer = 0;
            $scope.totalWay = 0;

            $.each(response, function (key, item) {
                $scope.totalServer += item.ServerHours;
                $scope.totalWay += item.WayHours;
            });
        }
    });


    $scope.showProcessDetail = function (process) {
        $scope.chkProcessDetail = process;
        $('#processDetail').modal('show');
    }

    $scope.printTask = function () {
        /*
        bdhtml = window.document.body.innerHTML;
        sprnstr = "<!--startprint-->";
        eprnstr = "<!--endprint-->";
        prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
        prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
        window.document.body.innerHTML = prnhtml;
        window.print();
        */
        $("div#taskDetial").jqprint();
    }
});

userApp.controller("ServerTimeStatController", function ($scope, $http, $filter, DataFormat) {
    $scope.start = "";
    $scope.end = "";

    var option = {
        series: [
            {
                name: '我的工时',
                type: 'pie',
                radius: '55%',
                data: []
            }
        ]
    };

    $scope.getDate = function () {
        var start = DataFormat.longDate($scope.start);
        var end = DataFormat.longDate($scope.end);

        $http.get("/Task/GetServerTimeStat?start=" + start + "&end=" + end).success(function (response) {
            if (response.error) {
                alert(response.error);
                console.log(response.error);
            } else {
                option.series[0].data = response;

                var myChart = echarts.init(document.getElementById('main'));
                myChart.setOption(option);
            }

        });
    }
});





taskApp.controller("crateTaskController", function ($scope, $http, $filter) {

    $http.get("/Task/GetCityList?parentId=0").success(function (json) {
        if (json.length > 0) {
            $scope.provinces = json;
        }
    });
    $scope.change = function (x) {
        console.log(x);
        var proId = $("#slcProv").val();

        $http.get("/Task/GetCityList?parentId=" + proId).success(function (json) {
            if (json.length > 0) {
                $scope.citys = json;
            }
        });
    };

    $http.get("/Account/GetCompanyUsers").success(function (json) {
        if (json.length > 0) {
            $scope.executeUsers = json;
        }
    });
});
