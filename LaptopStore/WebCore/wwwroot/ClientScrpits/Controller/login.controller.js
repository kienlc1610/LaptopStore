(function () {
    'use strict';

    angular
        .module('LoginApp')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$location', '$scope', 'AccountService', 'toastr', 'NgStorageService', '$window','$rootScope'];

    function LoginController($location, $scope, AccountService, toastr, NgStorageService, $window, $rootScope) {
        /* jshint validthis:true */
        var vm = $scope;
        var rvm = $rootScope;

        vm.login = login;

        activate();

        function activate() {
            if (NgStorageService.getSessionStorage("user") || rvm.user) {
                $window.location.href = '/';
            }
        }

        function login(account) {
            if (account) {
                var loginAccount = {
                    Email: account.email,
                    Password: account.password
                }

                AccountService.loginAccount(loginAccount)
                    .then(function (res) {
                        toastr.success('Login Successfully!');
                        var user = res;
                        NgStorageService.setSessionStorage('user', res);
                        if (user.isadmin) {
                            $window.location.href = '/Admin/Home';

                        } else {
                            $window.location.href = '/';
                        }
                    })
                    .catch(function (err) {
                        toastr.error(err.message);
                    });
            }
        }
    }
})();
