(function () {
    'use strict';

    angular
        .module('laptopStoreApp')
        .controller('HomeController', HomeController);

    HomeController.$inject = [
        '$location',
        'HomeService',
        '$scope',
        '$uibModal',
        '$q',
        '$rootScope',
        'NgStorageService',
        '$filter'
    ];

    function HomeController($location, HomeService, $scope, $uibModal, $q, $rootScope, ngStorageService, $filter) {
        /* jshint validthis:true */
        var vm = $scope;
        var rootScope = $rootScope;

        rootScope.carts = {
            
        };
        vm.latestProducts = null;
        vm.products = null;
        vm.filter = {};
        vm.numOfProducts = 0;
        vm.priceSlider = {
            options: {
                step: 1000000
            }
        };
        rootScope.removeProductFromCart = removeProductFromCart;
        vm.openProductDetail = openProductDetail;
        vm.addCart = addCart;
        vm.getProductByCategory = getProductByCategory;

        activate();
            
        rootScope.$watchCollection('carts.products', function (newValue, oldValue) {
            var total = 0;

            if (!angular.isUndefined(newValue) || newValue.length !== 0 || newValue !== null) {

                newValue.forEach(function (product) {
                    total += (product.price * product.quantity);
                });

                rootScope.carts.total = total;
            } else {
                rootScope.carts.total = total;
            }
        }, true);

        function activate() {
            var foundCarts = ngStorageService.getSessionStorage('carts');
            if (!angular.isUndefined(foundCarts)) {
                rootScope.carts.products = foundCarts
            } else {
                rootScope.carts.products = [];
            }

            getAllProducts();
            getFiveLatestProducts();
            getAllCategories();
            countProducts();
            getMinMaxPriceOfProduct();

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

        function getMinMaxPriceOfProduct() {
            var getMinValue = function () {
                HomeService.getMinPriceOfProduct()
                    .then(function (minPrice) {
                        vm.priceSlider.minValue = minPrice;
                        vm.priceSlider.options.floor = minPrice;
                        getMaxValue();
                    })
                    .catch(function (err) {
                        console.log(err);
                    });
            };

            var getMaxValue = function () {
                HomeService.getMaxPriceOfProduct()
                    .then(function (maxPrice) {
                        vm.priceSlider.maxValue = maxPrice;
                        vm.priceSlider.options.ceil = maxPrice;
                        vm.$broadcast('rzSliderForceRender');
                    })
                    .catch(function (err) {
                        console.log(err);
                    });
            };

            getMinValue();
        }

        function addCart(product) {
            var productInCart = ngStorageService.getSessionStorage('carts');

            if (angular.isUndefined(productInCart)) {
                product.quantity = 1;
                productInCart = [product]
                rootScope.carts.products.push(product);
            } else {
                if (productInCart.length === 0) {
                    product.quantity = 1;
                    productInCart.push(product);
                    rootScope.carts.products.push(product);
                } else {
                    var filter = $filter('filter')(productInCart, { productId: product.productId });
                    if (filter && filter.length !== 0) {
                        productInCart.forEach(function (p) {
                            if (p.productId === product.productId) {
                                p.quantity++;
                                rootScope.carts.total += (p.quantity * p.price);
                            }
                        });
                    } else {
                        product.quantity = 1;
                        productInCart.push(product);
                        rootScope.carts.products.push(product);
                    }
                }
                
            }
            ngStorageService.setSessionStorage('carts', productInCart);
            }

        function removeProductFromCart(product) {
            var productsInCart = ngStorageService.getSessionStorage('carts');
            var index = null;

            productsInCart.forEach(function (p, i) {
                if (p.productId === product.productId) {
                    index = i;
                }
            });

            productsInCart.splice(index, 1);
            rootScope.carts.products.splice(index, 1);
            ngStorageService.setSessionStorage('carts', productsInCart);
        }
    }
})();
