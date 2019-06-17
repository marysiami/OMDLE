(function () {
  'use strict';

  angular
    .module('app')
    .directive('userDirective', userDirective);

  function userDirective() {
    return {
      restrict: 'E',
      scope: {},
      templateUrl: 'app/templates/user.html'
    }
  };
})();
