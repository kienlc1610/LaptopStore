(function () {
    'use strict';

    angular
        .module('laptopStoreApp')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$location', 'HomeService', '$scope', '$uibModal'];

    function HomeController($location, HomeService, $scope, $uibModal) {
        /* jshint validthis:true */
        var vm = $scope;

        vm.latestProducts = null;
        vm.products = null;
        vm.filter = {};
        vm.numOfProducts = 0;

        vm.openProductDetail = openProductDetail;
        vm.getProductByCategory = getProductByCategory;

        activate();

        function activate() {
            getAllProducts();
            getFiveLatestProducts();
            getAllCategories();
            countProducts();

        }

        function getAllProducts() {
            
            HomeService.getAllProducts()
                .then(function (res) {
                    vm.products = res;
                })
                .catch(function (err) {
                    console.log(err);
                });
        }

        function getFiveLatestProducts() {
            HomeService.getFiveLatestProducts()
                .then(function (res) {
                    vm.latestProducts = res;
                })
                .catch(function (err) {
                    console.log(err);
                });
        }

        function getAllCategories() {
            HomeService.getAllCategories()
                .then(function (res) {
                    vm.categories = res;
                })
                .catch(function (err) {
                    console.log(err);
                });
        }
        
        function countProducts() {
            HomeService.countProducts()
                .then(function (numProducts) {
                    vm.numOfProducts = numProducts;
                })
                .catch(function (err) {
                    console.log(err);
                });
        }

        function openProductDetail(product) {

            vm.product = product;

            var modalInstance = $uibModal.open({
                templateUrl: '../Html/product-detail-popup.html',
                controller: 'ProductDetailController',
                size: 'lg',
                scope: $scope
               
            });
        }

        function getProductByCategory(cateId) {
            vm.filter.cateId = cateId;
            HomeService.getAllProducts(vm.filter)
                .then(function (foundProduct) {
                    vm.products = foundProduct;
                })
                .catch(function (err) {
                    console.log(err);
                });
        }
    }
})();
