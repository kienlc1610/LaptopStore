﻿(function () {
    'use strict';

    angular
        .module('laptopStoreApp')
        .controller('ProductDetailController', ProductDetailController);

    ProductDetailController.$inject = ['$location', '$scope', '$routeParams', 'HomeService'];

    function ProductDetailController($location, $scope, $routeParams, HomeService) {
        /* jshint validthis:true */
        var vm = $scope;

        var productId = $routeParams.id;
        vm.productDetail = vm.product;

        activate();

        function activate() {
            getProductDetail();
        }

        function getProductDetail() {
            HomeService.getProductDetail(productId)
                .then(function (product) {
                    vm.productDetail = product;
                })
                .catch(function (err) {
                    console.log(err);
                });
        }
    }
})();