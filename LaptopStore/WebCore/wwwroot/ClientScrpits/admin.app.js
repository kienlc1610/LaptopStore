(function () {
    'use strict';

    var app = angular.module('AdminApp', [
        // Angular modules 
        'ngRoute',
        'toastr',
        'ui.bootstrap'

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
            .when('/categories', {
                templateUrl: '/Html/Admin/Category/category.html',
                controller: 'AdminCategoryController'
            })
            .when('/categories/:id', {
                templateUrl: '/Html/Admin/Category/category-detail.html',
                controller: 'AdminCategoryDetail'
            })
            .when('/create/category', {
                templateUrl: '/Html/Admin/Category/category-create.html',
                controller: 'AdminCategoryCreate'
            })
            .when('/orders', {
                templateUrl: '/Html/Admin/Order/order.html',
                controller: 'AdminOrderController'
            })
            .when('/orders/:id', {
                templateUrl: '/Html/Admin/Order/order-detail.html',
                controller: 'AdminOrderDetail'
            })
            .when('/create/order', {
                templateUrl: '/Html/Admin/Order/order-create.html',
                controller: 'AdminOrderCreate'
            })
            .otherwise({
                redirectTo: '/'
            });
    }]);
})();