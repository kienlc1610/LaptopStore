(function () {
    'use strict';

    angular
        .module('LoginApp')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$location', '$scope', 'AccountService', 'toastr', 'NgStorageService'];

    function LoginController($location, $scope, AccountService, toastr, NgStorageService) {
        /* jshint validthis:true */
        var vm = $scope;

        vm.login = login;

        activate();

        function activate() { }

        function login(account) {
            if (account) {
                var loginAccount = {
                    Email: account.email,
                    Password: account.password
                }

                AccountService.loginAccount(loginAccount)
                    .then(function (res) {
                        toastr.success('Login Successfully!');
                        NgStorageService.setSessionStorage('user', res);
                    })
                    .catch(function (err) {
                        toastr.error(err.message);
                    });
            }
        }
    }
})();
