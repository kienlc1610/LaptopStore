(function () {
    'use strict';

    var app = angular.module('laptopStoreApp', [
        // Angular modules 
        'ngRoute',
        'ngSanitize',
        'ui.bootstrap',
        'angularUtils.directives.dirPagination',
        'fancyboxplus',
        'rzModule'

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
            .otherwise({
                redirectTo: '/'
            });
    }]);

})();