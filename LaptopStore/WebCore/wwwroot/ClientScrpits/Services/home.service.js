(function () {
    'use strict';

    angular
        .module('laptopStoreApp')
        .factory('HomeService', HomeService);

    HomeService.$inject = ['$http', '$q'];

    function HomeService($http, $q) {
        var service = {
            getAllProducts: getAllProducts,
            getFiveLatestProducts: getFiveLatestProducts,
            getAllCategories: getAllCategories,
            countProducts: countProducts,
            getProductDetail: getProductDetail,
        };

        return service;

        function getAllProducts(filter) {
            var deferred = $q.defer();
            var api = 'http://localhost:49595/api/Products/?'

            if (filter) {
                if (filter.cateId) {
                    api = api + '&CateID=' + filter.cateId;
                }

                if (filter.priceFrom) {
                    api = api + '&PriceFrom=' + filter.priceFrom;
                }

                if (filter.priceTo) {
                    api = api + '&PriceTo=' + filter.priceTo;
                }

                if (filter.discount) {
                    api = api + '&Discount=' + filter.discount;
                }

                if (filter.pageIndex) {
                    api = api + '&pageIndex= ' + filter.pageIndex;
                }
            }

            $http.get(api)
                .then(function (foundProducts) {
                    deferred.resolve(foundProducts.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function getFiveLatestProducts() {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/Products/Latest')
                .then(function (foundProducts) {
                    deferred.resolve(foundProducts.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function getAllCategories() {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/Categories')
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function countProducts() {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/Products/Count')
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function getProductDetail(productId) {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/Products/' + productId)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;

        }
    }
})();