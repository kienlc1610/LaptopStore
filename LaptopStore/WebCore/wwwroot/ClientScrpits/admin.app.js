(function () {
    'use strict';

    var app = angular.module('AdminApp', [
        // Angular modules 
        'ngRoute',
        'toastr'

        // Custom modules 

        // 3rd Party Modules

    ]);

    app.config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/products', {
                templateUrl: '/Html/Admin/Product/products.html',
                controller: 'AdminProductController'
            })
            .when('/products/:id', {
                templateUrl: '/Html/Admin/Product/product-detail.html',
                controller: 'AdminProductDetail'
            })
            .when('/create/product', {
                templateUrl: '/Html/Admin/Product/product-create.html',
                controller: 'AdminProductCreate'
            })
            .otherwise({
                redirectTo: '/'
            });
    }]);
})();