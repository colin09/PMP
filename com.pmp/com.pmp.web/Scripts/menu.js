angular.module('userMenuApp', [])
 .controller("MenuItemCtl1", function ($scope, $http) {
     $http.get("../scripts/userLevel.json")
         .success(function (json) {
             if (json.length > 0) {
                 var level = $("#userLevel").val();
                 $.each(json, function (key, item) {
                     if (item.userLevel.indexOf(level + ',') >= 0) {
                         $scope.body = item;
                     }
                 });
             }
         });
 })