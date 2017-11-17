(function () {
    'use strict';

    angular
        .module('AdminApp')
        .factory('AdminService', HomeService);

    HomeService.$inject = ['$http', '$q'];

    function HomeService($http, $q) {
        var service = {
            getAllProducts: getAllProducts,
            deleteProduct: deleteProduct,
            getAllCategories: getAllCategories,
            updateProduct: updateProduct,
            getProductDetail: getProductDetail,
            createProduct: createProduct,
            getMaxPriceOfProduct: getMaxPriceOfProduct,
            createCustomer: createCustomer,
            createOrder: createOrder,
            createOrderDetail: createOrderDetail
        };

        return service;

        function getAllProducts() {
            var deferred = $q.defer();
            var api = 'http://localhost:49595/api/Products/'

            $http.get(api)
                .then(function (foundProducts) {
                    deferred.resolve(foundProducts.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function deleteProduct(id) {
            var deferred = $q.defer();

            $http.delete('http://localhost:49595/api/Products/' + id)
                .then(function (res) {
                    deferred.resolve(res.data);
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

        function updateProduct(product) {
            var deferred = $q.defer();

            $http.put('http://localhost:49595/api/Products/' + product.productId, product)
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

        function createProduct(product) {
            var deferred = $q.defer();

            $http.post('http://localhost:49595/api/Products/Create', product, { header: { 'Content-Type': 'application/json' } })
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function getMaxPriceOfProduct() {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/Products/Price/Min')
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function createCustomer(customer) {
            var deferred = $q.defer();

            $http.post('http://localhost:49595/api/Customers/Create', customer)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function createOrder(order) {
            var deferred = $q.defer();

            $http.post('http://localhost:49595/api/Orders', order)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function createOrderDetail(orderDetail) {
            var deferred = $q.defer();

            $http.post('http://localhost:49595/api/OrderDetails', orderDetail)
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