var app = angular.module("addressApp", []);

app.controller("AddressController", function ($scope, $http) {
    $scope.formData = {};
    $scope.districts = [];
    $scope.cities = [];

    // Load districts on page load
    $http.get('/Address/GetDistricts').then(function (res) {
        $scope.districts = res.data;
    });

    // Load cities when district changes
    $scope.loadCities = function () {
        if ($scope.formData.DistID) {
            $http.get('/Address/GetCitiesByDistrict/' + $scope.formData.DistID).then(function (res) {
                $scope.cities = res.data;
            });
        }
    };

    // Save address
    $scope.saveAddress = function () {
        $http.post('/Address/SaveAddress', $scope.formData)
            .then(function (response) {
                alert("Address saved successfully!");
                $scope.formData = {};
            }, function (error) {
                alert("Error saving address.");
            });
    };
});
