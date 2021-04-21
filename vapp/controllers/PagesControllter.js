stpaulsApp.controller("PagesController", ['GlobalVariableService', '$scope', '$filter', '$http', '$location', '$routeParams', 'toaster', 'CommonService', 'uiGridConstants',
    function (GlobalVariableService,$scope, $filter, $http, $location, $routeParams, toaster, CommonService, uiGridConstants) {
    $scope.ShowSpinnerStatus = false;

    $scope.showSpinner = function () {
        $scope.ShowSpinnerStatus = true;
    }
    $scope.hideSpinner = function () {
        $scope.ShowSpinnerStatus = false;
    }
    $scope.searchCategory = '';
    $scope.searchDisplayName = '';
    $scope.searchDescription = '';

    $scope.TodaysDate = $filter('date')(new Date(), "dd/MM/yyyy");
    $scope.ID = $routeParams.ID;
    $scope.MaterialList = [];
    $scope.EditMaterial = {};
    $scope.submitted = false;
    //$scope.ItemCatogoryList = [];
    $scope.Material = {
          Product: null
        , Model: null
        , Size1: null
        , Size2: null
        , StdPkg: null
        , BoxQty: null
    };
    $scope.gridOptions = {
        enableFiltering: true,
        enableCellEditOnFocus: false,
        enableRowSelection: false,
        enableRowHeaderSelection: false,
        enableSelectAll: false,
        enableColumnResizing: true,
        showColumnFooter: false,
        paginationPageSizes: [25, 50, 75],
        paginationPageSize: 25,
        columnDefs: [
            
            { name: 'No.', field: 'SrNo', width: 50, visible: false, enableFiltering: false, enableSorting: true, headerCellClass: 'text-right', cellClass: 'text-right', displayName: '#', cellTemplate: '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row)+(grid.options.paginationPageSize*(grid.options.paginationCurrentPage-1))+1}}</div>' },
            { width:250, displayName: 'Title', field: 'PageHeader', cellTooltip: true, enableCellEdit: false, cellClass: 'text-left', headerCellClass: 'text-center' },
            { width:250, displayName: 'body', field: 'PageBody', enableCellEdit: false, enableCellEditOnFocus: true, cellTooltip: true, cellClass: 'text-left', headerCellClass: 'text-center' },
            { width:250, displayName: 'Version', field: 'Version', enableCellEdit: false, enableCellEditOnFocus: true, cellTooltip: true, cellClass: 'text-left', headerCellClass: 'text-center' },
            {
                name: 'Action', width: 80, enableFiltering: false, cellClass: 'text-center', displayName: 'Action', cellTemplate: '<div class="ui-grid-cell-contents">'
                    + '<a id="btnView" type="button" title="Edit" style="line-height: 0.5;" class="btn btn-primary btn-xs" href="#EditPage/{{row.entity.PageId}}" ><span data-feather="edit"></span> </a>'
                    + '</div><script>feather.replace()</script>'
            },
        ],
        data: []
    };

    $scope.SaveMaterials = function (isFormValid) {
        try {
            var isValid = isFormValid;
            $scope.submitted = !isValid;
            var strItemCategoryName = $scope.Material.ItemCategory.ItemCategoryName;
            var strItemDescription = $scope.Material.Description;

            if (isValid) {
                var values = {
                    "Descriptioin": $scope.Material.Description
                    //, "Unit": $scope.Material.Unit
                    //, "NoOfPiecePerUnit": $scope.Material.NoOfPiecePerUnit.toString()
                    , "RetailRate": $scope.Material.RetailRate.toString()
                    , "WholeSaleRate": $scope.Material.WholeSaleRate.toString()
                    , "CostingPrice": $scope.Material.CostingPrice.toString()
                    //, "QuantityInHand": $scope.Material.QuantityInHand.toString()
                    , "ReorderLevel": $scope.Material.ReorderLevel.toString()
                    , "CreatedOn":new Date()
                    , "Product": $scope.Material.Product
                    , "Model": $scope.Material.Model
                    , "Size1": $scope.Material.Size1
                    , "Size2": $scope.Material.Size2
                    , "StdPkg": $scope.Material.StdPkg == null ? null :$scope.Material.StdPkg.toString()
                    , "BoxQty": $scope.Material.BoxQty == null ? null :$scope.Material.BoxQty.toString()
                    , "ItemCategoryId": $scope.Material.ItemCategory.ItemCategoryId
                    , "DisplayName": strItemCategoryName.substring(0, strItemCategoryName.indexOf(' ')) + '-' + strItemDescription.substring(0, strItemDescription.indexOf(' ')) + ' ' + $scope.Material.Model + ' ' + $scope.Material.Size1 + ' ' + $scope.Material.Size2
                };
                CommonService.PostData("Materials", values).then(function (response) {
                    console.log("response " + response);
                    if (response.MaterialId > 0) {
                        toaster.pop('success', "", "Material Saved Successfully", 5000, 'trustedHtml');
                        $scope.RedirectDashboard();
                    }
                }, function (data) {
                    console.log(data);
                });
            }
        } catch (error) {
            console.log("Exception caught in the SaveSupplierRetailers function. Exception Logged as " + error.message);
        }
    };

    //Update Materials
    $scope.UpdateMaterials = function (isFormValid) {
        try {
            var isValid = isFormValid;
            $scope.submitted = !isValid;
            //var strItemCategoryName = $scope.EditMaterial.ItemCategory.ItemCategoryName;
            //var strItemDescription = $scope.EditMaterial.Descriptioin;

            if (isValid) {
                var values = {
                    "PageHeader": "Header test"
                    , "PageBody": "Body TEst"
                    , "UpdateDate": new Date()
                    , "Version": "1"
                    , "PageNameId": 1
                    , "Active":1
                };
                CommonService.UpdateData("Pages", values, 1).then(function (response) {
                    console.log("response " + response);
                    if (response != undefined) {
                        toaster.pop('success', "", "Pages Data Updated Successfully", 5000, 'trustedHtml');
                        //$scope.RedirectDashboard();
                    }
                }, function (data) {
                    console.log(data);
                });
            }
        } catch (error) {
            console.log("Exception caught in the SaveSupplierRetailers function. Exception Logged as " + error.message);
        }
    };

    $scope.GetItemCategory = function (callback) {
        var postData = {
            title: "ItemCategories",
            fields: ["*"]
        };
        CommonService.GetListItems(postData).then(function (response) {
            if (response && response.data.d.results.length > 0) {
                $scope.ItemCatogoryList = response.data.d.results;
            }
            callback();
        });
    };
    $scope.GetDataForDashboard = function () {
        $scope.WholeSaleList = [];
        var lstMaterial = {
            title: "Pages",
            fields: ["*"],
            //lookupFields: ["ItemCategory"],
            //filter:["1 eq 1"],
            limitTo: 20,
            //orderBy: "CreatedOn desc"
        };
        if ($scope.searchDisplayName !=='') {
            lstMaterial.filter = lstMaterial.filter + " and indexof(DisplayName,'" + $scope.searchDisplayName + "') gt -1";
        }
        if ($scope.searchDescription !== '') {
            lstMaterial.filter = lstMaterial.filter + " and indexof(Descriptioin,'" + $scope.searchDescription + "') gt -1";
        }
        if ($scope.searchCategory > 0) {
            lstMaterial.filter = lstMaterial.filter + " and ItemCategoryId eq " + $scope.searchCategory;
        }

        $scope.showSpinner();
        CommonService.GetListItems(lstMaterial).then(function (response) {
            if (response && response.data.d.results.length > 0) {
                $scope.MaterialList = response.data.d.results;

            }
            else {
                $scope.MaterialList = [];

            }
            $scope.gridOptions.data = $scope.MaterialList;
            $scope.hideSpinner();
        });
    };
    $scope.GetMaterialsList = function () {
        var lstBill = {
            title: "Materials",
            fields: ["*","ItemCategory/ItemCategoryName"],
            lookupFields: ["ItemCategory"],
            limitTo:20,
            orderBy: "CreatedOn desc"
        };
        $scope.showSpinner();
        CommonService.GetListItems(lstBill).then(function (response) {
            if (response && response.data.d.results.length > 0) {
                $scope.MaterialList = response.data.d.results;
                $scope.gridOptions.data = $scope.MaterialList;
                $scope.hideSpinner()
                
            }
        });
    };

    $scope.GetMaterialById = function () {
        var lstBill = {
            title: "Pages",
            fields: ["*"], 
            filter: ["PageId eq 1"],// + $scope.ID],
            //orderBy: "CreatedOn desc"
        };
        CommonService.GetListItems(lstBill).then(function (response) {
            if (response && response.data.d.results.length > 0) {
                $scope.EditMaterial = response.data.d.results[0];
            }
        });
    };

    $scope.RedirectDashboard = function () {
        $location.path('/Materials');
    };

    $scope.init = function () {

        //GlobalVariableService.validateUrl($location.$$path);
        if ($scope.ID > 0) {
            $scope.GetMaterialById();
        }
        //$scope.GetItemCategory(function () {
        //    if ($scope.ID > 0) {
        //        $scope.GetMaterialById();
        //    }
        //    else {
        //        $scope.GetMaterialsList();
        //    }
        //});
        
    };

    $scope.init();
}]);