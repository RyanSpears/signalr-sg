(function () {
    'use strict';

    angular
        .module('app')
        .controller('mainCtrl', mainCtrl);

    mainCtrl.$inject = ['$q', '$scope'];

    function mainCtrl($q, $scope) {
        /* jshint validthis:true */
        var vm = this;

        vm.messages = [];

        vm.title = 'mainCtrl';

        initHub();

        return vm;

        function initHub() {

            $.connection.hub.url = "http://localhost:6024/signalr";

            /* Declare a proxy to reference the hub.*/
            var firstHub = $.connection.firstHub;

            firstHub.client.clientMessageFromServer = clientMessage;

            $.connection.hub.start().done(function () {
                firstHub.server.hubReceiveMessage('Hello from angular web application');
            });
        }

        function clientMessage(message) {
            vm.messages.push(message);
            console.log(message + ': number ' + vm.messages.length);
            $scope.$apply();
        }
    }
})();
