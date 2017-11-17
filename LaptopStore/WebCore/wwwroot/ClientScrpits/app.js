(function () {
    'use strict';

    var app = angular.module('laptopStoreApp', [
        // Angular modules 
        'ngRoute',
        'ngSanitize',
        'ui.bootstrap',
        'angularUtils.directives.dirPagination',
        'fancyboxplus',
        'rzModule',
        'ngStorage'

        // Custom modules 

        // 3rd Party Modules

    ]);

    app.config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: '/Html/home.html',
                controller: 'HomeController'
            })
            .when('/products/:id', {
                templateUrl: 'Html/product-detail.html',
                controller: 'ProductDetailController'
            })
            .when('/cart', {
                templateUrl: 'Html/cart.html',
                controller: 'CartController'

            })
            .when('/checkout', {
                templateUrl: 'Html/checkout.html',
                controller: 'CheckOutController'
            })
            .otherwise({
                redirectTo: '/'
            });
    }]);

    app.run(['NgStorageService', '$rootScope', function (ngStorageService,$rootScope) {
        $rootScope.carts = {};

        $rootScope.removeProductFromCart = removeProductFromCart;

        $rootScope.$watchCollection('carts.products', function (newValue, oldValue) {
            var total = 0;

            if (!angular.isUndefined(newValue) || newValue.length !== 0 || newValue !== null) {

                newValue.forEach(function (product) {
                    total += (product.price * product.quantity);
                });

                $rootScope.carts.total = total;
            } else {
                $rootScope.carts.total = total;
            }
        }, true);

        function removeProductFromCart(product) {
            var productsInCart = ngStorageService.getSessionStorage('carts');
            var index = null;

            productsInCart.forEach(function (p, i) {
                if (p.productId === product.productId) {
                    index = i;
                }
            });

            productsInCart.splice(index, 1);
            $rootScope.carts.products.splice(index, 1);
            ngStorageService.setSessionStorage('carts', productsInCart);
        }

        var foundCarts = ngStorageService.getSessionStorage('carts');
        if (!angular.isUndefined(foundCarts)) {
            $rootScope.carts.products = foundCarts
        } else {
            $rootScope.carts.products = [];
        }

    }]);

})();