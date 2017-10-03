(function () {
    'use strict';

    angular
        .module('CSINVENTORY')
        .controller('CustomerController', CustomerController)

    CustomerController.$inject = ['$scope', 'CustomerService'];

    function CustomerController($scope, CustomerService) {
        //#region Declarations
        var vm = this;
        //#endregion Declarations

        //#region Bindable members
        vm.Initialize = _initialize;
        vm.Save = _save;
        vm.IsWorking = false;
        
        
        //#endregion Bindable members

        function _initialize() {
            vm.IsWorking = true;
        }

        function _save() {
            var dto = {};
            
            dto = {
                CustomerCode: vm.CustomerCode,
                CustomerName: vm.CustomerName,
                CustomerAddress: vm.CustomerAddress
            };
            debugger;
            return CustomerService
                .Save(dto)
                .then(function (data) {
                    if (data != null) {
                        
                    }
                }, function (reason) {
                    //doOnError("GetPReviewYears", reason);
                });
        }
    }

}());