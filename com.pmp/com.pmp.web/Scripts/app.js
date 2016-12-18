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


userApp.controller("UserTaskDetailController", function ($scope, $http, $filter) {
    $scope.showProcess = false;
    var id = $("#projectId").val();
    $http.get("/Task/GetTaskProcess?id=" + id).success(function (response) {
        if (response.error) {
            console.log(response.error);
        } else {
            $scope.showProcess = true;
            $scope.processList = response;
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
