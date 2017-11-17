(function () {
    'use strict';

    angular
        .module('AdminApp')
        .controller('AdminCategoryController', AdminProductController);

    AdminProductController.$inject = ['$location', 'AdminService', 'toastr', '$scope'];

    function AdminProductController($location, AdminService, toastr, $scope) {
        /* jshint validthis:true */
        var vm = $scope;

        vm.deleteCategory = deleteCategory;

        activate();

        function activate() {
            getAllCategory();
        }

        function getAllCategory() {
            AdminService.getAllCategories()
                .then(function (category) {
                    vm.categories = category;
                })
                .catch(function (err) {
                    toastr.error('Error:' + JSON.stringify(err));
                });
        }

        function deleteCategory(id) {
            AdminService.deleteCategory(id)
                .then(function (products) {
                    toastr.success("Category" + id + "is deleted!");
                    activate();
                })
                .catch(function (err) {
                    toastr.error('Error:' + JSON.stringify(err));
                });
        }
    }
})();
