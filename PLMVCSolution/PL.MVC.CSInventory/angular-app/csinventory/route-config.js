(function () {
    'use strict';

    var app = angular.module('CSINVENTORY', ['ngRoute']);

    app.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/customer', {
            templateUrl: '/angular-app/csinventory/customer/views/index.html',
            controller: 'CustomerController',
            controllerAs: 'CustCtrl'
        });

    }]);
}());