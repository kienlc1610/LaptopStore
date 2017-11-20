(function () {
    'use strict';

    angular
        .module('AdminApp')
        .controller('AdminProductCreate', AdminProductCreate);

    AdminProductCreate.$inject = ['$location', 'AdminService', 'toastr', '$scope'];

    function AdminProductCreate($location, AdminService, toastr, $scope) {
        /* jshint validthis:true */
        var vm = $scope;

        vm.categories = [];
        vm.createProduct = {};
        vm.doCreateProduct = doCreateProduct;


        activate();

        function activate() {
            getAllCategory();
        }

        function getAllCategory() {
            AdminService.getAllCategories()
                .then(function (foundCate) {
                    vm.categories = foundCate;
                })
                .catch(function (err) {
                    toastr.error("Error:" + JSON.stringify(err));
                });

        }

        function doCreateProduct(product) {
            var obj =  {
                CateId: product.cateId.cateId,
                Name: product.name,
                Price: product.price,
                Image: 'no-img.jpg' ,
                Description: product.description,
                Quantity: product.quantity,
                Discount: product.discount,
            };

            if (product.status == 'true') {
                obj.Status = true;
            } else {
                obj.Status = false;
            }

            AdminService.createProduct(obj)
                .then(function (res) {
                    toastr.success("Create Product Successfully!");
                    $location.path('/#!/products');
                    $location.replace();
                })
                .catch(function (err) {
                    toastr.error("Error:" + JSON.stringify(err));
                });
        }
    }
})();
