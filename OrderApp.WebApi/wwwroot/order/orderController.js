var orderApp = angular.module('orderApp', ['ngRoute']);

orderApp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    
    $locationProvider.hashPrefix('');
    $routeProvider.when('/new_order', {
        templateUrl: 'order/order.html',
        controller: 'OrderController'
    }).otherwise({
        redirectTo: '/'
    });

}]);

orderApp.controller('OrderController', ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    $scope.loading = true;
    $scope.orderArray = [];
    $scope.getAvailableProducts = function () {
        $http.get("http://localhost:51268/api/product/get_all_available_async").then(function (res) {
            $scope.loading = false;
            $scope.productArray = res.data.data;
        }).catch(function (err) {
            $scope.loading = false;
        })
    };
    $scope.getAvailableProducts();

    $scope.order = {};

    $scope.addToCard = function (product) {
        if (parseInt(product.InStock) - 1 < 0) {
            alert('We do not have more from ' + product.Name + ' sorry :/');
        }
        if ($scope.orderArray.length > 0) {
            var productInOrder = $filter('filter')($scope.orderArray, { productId: product.Id }, true)[0];
            if (productInOrder) {
                productInOrder.quantity++;
            } else {
                $scope.orderArray.push({
                    productId: product.Id,
                    productName: product.Name,
                    productPrice: product.Price,
                    quantity: 1
                });
            }
        } else {
            $scope.orderArray.push({
                productId: product.Id,
                productName: product.Name,
                productPrice: product.Price,
                quantity: 1
            });
        }
        product.InStock = parseInt(product.InStock) - 1;
        product.in_order = product.in_order > 0 ? parseInt(product.in_order) + 1 : 1;
    }
}]);