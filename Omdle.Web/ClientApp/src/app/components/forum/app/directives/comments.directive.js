(function () {
  'use strict';

  angular
    .module('app')
    .directive('commentsDirective', commentsDirective);

  function commentsDirective() {
    return {
      restrict: 'EA',
      templateUrl: 'app/templates/comments.html'
    }
  }
})();
