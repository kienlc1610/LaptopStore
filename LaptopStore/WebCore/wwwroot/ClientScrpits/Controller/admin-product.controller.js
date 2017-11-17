(function () {
    'use strict';

    angular
        .module('AdminApp')
        .controller('AdminProductController', AdminProductController);

    AdminProductController.$inject = ['$location', 'AdminService', 'toastr', '$scope'];

    function AdminProductController($location, AdminService, toastr, $scope) {
        /* jshint validthis:true */
        var vm = $scope;

        vm.deleteProduct = deleteProduct;

        activate();

        function activate() {
            getAllProducts();
        }

        function getAllProducts() {
            AdminService.getAllProducts()
                .then(function (products) {
                    vm.products = products;
                })
                .catch(function (err) {
                    toastr.error('Error:' + JSON.stringify(err));
                });
        }

        function deleteProduct(id) {
            AdminService.deleteProduct(id)
                .then(function (products) {
                    toastr.success("Product" + id + "is deleted!");
                    activate();
                })
                .catch(function (err) {
                    toastr.error('Error:' + JSON.stringify(err));
                });
        }
    }
})();
