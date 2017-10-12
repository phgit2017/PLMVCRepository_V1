(function () {
    'use strict';

    angular
        .module('CSINVENTORY')
        .factory('CustomerService', CustomerService);

    CustomerService.$inject = ['$q','$http'];


    function CustomerService($q, $http) {
        //#region Declarations and Initialization
        var CustomerServiceFactory = {},
            apiBaseUrl = '/maintenancecustomer';
        //#endregion Declarations and Initialization

        //#region Bindable members
        CustomerServiceFactory.Save = _save;

        return CustomerServiceFactory;
        //#endregion Bindable members

        //#region Public methods
        function _save(dto) {
            var deferred = $q.defer(), url, error = '';
            url = apiBaseUrl + '/SaveCustomer';


            $http
                .post(url,dto)
                .success(function (result) {
                    if (result === null) {
                        result = [];
                    }

                    deferred.resolve(result);
                })
                .error(function (e, status) {
                    deferred.reject(e);
                });

            return deferred.promise;

        }
        //#endregion Public methods

        //#region Private methods

        //#endregion Private methods
    }

}());