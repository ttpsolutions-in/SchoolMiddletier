stpaulsApp.factory('PrintService', ['$location', '$http', '$alert', '$filter', 'Config', 'ExceptionHandler', 'CommonService',
    function ($location, $http, $alert, $filter, Config, ExceptionHandler, CommonService) {

    var PrintService = {};
    var Image = [];
    //var GSTPercentage = [];
    var AllCreditBillNos = [];
    var AllCreditBillDetail = [];
    var SupplierRetailers = [];
    var Category = {
        Supplier: 1,
        Customer: 2
    };
    //var Image.TotalAmount = 0;

    PrintService.printBill = function () {
        var ImageOrRetail = '';
        var colspan = 0;
        var content = "";
        var header = '';
        var body = '';
        var footer = '';
        var strOneBillContent = ''
        var WholeHeader = '';
        var WholeFooter = '';
        WholeHeader = '<html>' +
            '<head><title>Print Preview</title>' +
            '<link href="vContent/style/bootstrap.min.css" rel="stylesheet" />  ' +
            '<script type="text/javascript" src="vContent/Scripts/angular.min.js"></script>' +           
            //'<style>@media print {'+
            //        '.footer { page -break-after: always; }' +
            //'}</style></head>' +
            '<body>' +
            '<div id="dv" style="margin:25px">  ';
        var previousBillNo = 0;
        var slno = 0;
        angular.forEach(Image, function (billDetail, key) {
            //parseInt(key) + parseInt(1);
            var currentBillNo = billDetail.BillNo;
            // data fetched sorted by billno from database
            // current and previous billno not equal means we need to prepare a new bill.
            slno = parseInt(slno) + parseInt(1);
            if (currentBillNo != previousBillNo) {
                slno = 0;
                slno = parseInt(slno) + parseInt(1);

                if (key != 0) {
                    strOneBillContent += header + body + footer;
                    header = '';
                    body = '';
                    footer = '';
                    
                }

                if (billDetail.SaleCategoryId == 1) {
                    ImageOrRetail = 1
                    colspan = 6
                }
                else {
                    colspan = 4
                    ImageOrRetail = 2
                }

                header += '<div id="dvPrintBill" style="margin:150px" class="mt-5"><div class="row">  ' +
                    '<div class="col-md-12 text-center">  ' +
                    '<h3 style="text-align:center">EPHRAIM TRADERS</h3>  ' +
                    '<span>Churachandpur, Manipur - 795125</span><br />  '
                if (billDetail.ShowGSTNo == 1) {
                    header += '<span>GSTIN - 14BKYPT3527Q1Z2</span> '
                }
                header += '<hr class="newhr" />  ' +
                    '</div>  ' +
                    '<div class="col-md-12">  ' +
                    '<span><b>Name: ' + billDetail.Name + ' </b></span>  ' +
                    '<span class="float-right"><b>Bill No.: ' + billDetail.BillNo + '</b></span><br />  ' +
                    '<span><b>Contact No.: ' + billDetail.Contact + '</b></span>  ' +
                    '<span class="float-right"><b>Bill Date: ' + $filter('date')(billDetail.SaleDate, "dd/MM/yyyy") + '</b></span>  ' +
                    '</div>  ' +
                    '</div>  ' +
                    '<hr />  ' +
                    '<div class="form-row">  ' +
                    '<div class="col-md-12 nopadding">  ' +
                    '<table class="table table-sm table-striped table-bordered table-condensed">  ' +
                    '<tr>  ' +
                    '<th style="text-align:center">No.</th>  ' +
                    '<th style="text-align:center">Particular</th>  ' +
                    '<th style="text-align:center">Rate</th>  ' +
                    '<th style="text-align:center">Quantity</th>'
                if (ImageOrRetail == 1) {
                    header += '<th style="text-align:center">Discount</th>  ' +
                        '<th style="text-align:center">DLP</th>'
                }

                header += '<th style="text-align:center">Amount</th>  ' +
                    '</tr>  ';

                //if (slno % 2 == 0)
                //    body += '</table > <div><p style="page-break-before: always"></div>< table class="table table-sm table-striped table-bordered table-condensed" >';

                body += '<tr><td align="right">' + slno + '</td> ' +
                    '<td>' + billDetail.DisplayName + '</td> ' +
                    '<td align="right">' + billDetail.Rate + '</td>  ' +
                    '<td align="right">' + billDetail.Quantity + '</td>  '
                if (ImageOrRetail == 1) {
                    body += '<td align="right">' + billDetail.Discount + '</td>' +
                        '<td align="right">' + billDetail.DLP + '</td>  '
                }
                body += '<td align="right">' + billDetail.Amount + '</td></tr>';
                

                footer += '<tr>  ' +
                    '<td colspan="' + colspan + '" class="text-right">Total Amount</td>  ' +
                    '<td class="text-right">' + parseFloat(billDetail.TotalAmount).toFixed(2) + '</td>  ' +
                    '</tr>  ' +
                    '<tr>  ' +
                    '<td colspan="' + colspan + '" class="text-right">  ' +
                    'GST (' + billDetail.GSTPercentage + ' %)' +
                    '</td>  ' +
                    '<td class="text-right"><span>' + billDetail.GSTAmount + '</span></td>  ' +
                    '</tr>  ' +
                    '<tr>  ' +
                    '<td colspan="' + colspan + '" class="text-right"> Grand Total</td>  ' +
                    '<td class="text-right">' + billDetail.GrandTotal + '</td>' +
                    '</tr>  ' +
                    '</table>  ' +
                    '<br />  ' +
                    '<span>Customer Signature</span>  ' +
                    '<span class="float-right">For <b>EPHRAIM TRADERS</b><br /><br /> Authorised Signatory</span>  ' +
                    '</div>  ' +
                    '</div>  ' +
                    '</div> <div><p style="page-break-after: always"></div> '


            } //end of if billno is same.
            else if (currentBillNo == previousBillNo) {
                //var i;
                //for (i = 0; i < billDetail.length; i++) {
                //if (slno % 2 == 0)
                //    body += '</table><div><p style="page-break-before: always"></div>< table class="table table-sm table-striped table-bordered table-condensed" >'
                body += '<tr><td align="right">' + slno + '</td> ' +
                    '<td>' + billDetail.DisplayName + '</td> ' +
                    '<td align="right">' + billDetail.Rate + '</td>  ' +
                    '<td align="right">' + billDetail.Quantity + '</td>  '
                if (ImageOrRetail == 1) {
                    body += '<td align="right">' + billDetail.Discount + '</td>' +
                        '<td align="right">' + billDetail.DLP + '</td>  '
                }
                body += '<td align="right">' + billDetail.Amount + '</td></tr>';
                

            }
            previousBillNo = currentBillNo;
        });

        strOneBillContent += header + body + footer;

        //WholeFooter += '<table style="width:100%"><tr><td><button type="button" class="btn btn-primary col-md-offset-5" ng-click="Export()"> <span data-feather="printer"></span>Export to PDF</button></td></tr></table>
        WholeFooter +='</body ></html > ';
        content = WholeHeader + strOneBillContent + WholeFooter;


        var mywindow = window.open('', 'PRINT', 'height=800,width=800');
        
            mywindow.document.write(content);
        setTimeout(function () {
            mywindow.document.close();
            mywindow.focus();

            mywindow.print();
            mywindow.close();
        }, 1000);
        
        //$timeout(function () {
        $location.path('/');
        //AllCreditBillDetail = null;
    };
    PrintService.GetCustomerBillsNPrint = function (CustomerId) {
        PrintService.GetImageByID(CustomerId,0, function () {
            PrintService.printBill();
        });
    }
    PrintService.GetSingleBillNPrint = async function (BillNo) {
        //return promise = new Promise((resolve, reject) => {
                   
        await PrintService.GetImageByID(0, BillNo);//, function () {
        await PrintService.printBill();
        //});
        //return promise;
    }
    PrintService.GetImageByID = function (customerId,billNo, callback) {

        var lstBill = {
            title: "BillDetailsViews",
            fields: [
                "BillNo",
                "Name",
                "SaleDate",
                "RetailerId",
                "MaterialId",
                "DisplayName",
                "Quantity",
                "Discount",
                "DLP",
                "Amount",
                "GodownName",
                "GodownId",
                "GSTApplied",
                "SaleTypeId",
                "SaleCategoryId",
                "GSTPercentage",
                "GSTAmount",
                "DiscountApplied",
                "TotalAmount",
                "GrandTotal",
                "ShowGSTNo",
                "Contact",
                "Rate",
                "Active",
                "BillStatus"
            ],
            //lookupFields: ["Bill", "Material", "Godown","Material/SupplierRetailer"],
            //filter: ["RetailerId eq " + customerId],
            //limitTo: "5000",
            orderBy: "BillNo"
        };
        if (customerId == 0)
            lstBill.filter = "BillNo eq " + billNo
        else
            lstBill.filter = "RetailerId eq " + customerId

        var promise =CommonService.GetListItems(lstBill).then(function (response) {
            if (response && response.data.d.results.length > 0) {
                Image = response.data.d.results;
                if (callback)
                    callback();
            }
        });
        return promise;
    };
    PrintService.GetSupplierCustomer = function (callback) {
        var lstBill = {
            title: "SupplierRetailers",
            fields: ["*"],
            //lookupFields: ["SupplierRetailer", "SaleCategory", "SaleType", "Status"],
            filter: ["Category eq " + 2],
            //limitTo: "5000",
            orderBy: "CreatedOn desc"
        };
        CommonService.GetListItems(lstBill).then(function (response) {
            if (response && response.data.d.results.length > 0) {
                SupplierRetailers = response.data.d.results;

            }
        });
        callback();
    };

    PrintService.GetGSTPercentage = function (callback) {
        var lstBill = {
            title: "GSTPercentages",
            fields: ["*"]
        };
        CommonService.GetListItems(lstBill).then(function (response) {
            if (response && response.data.d.results.length > 0) {
                GSTPercentage = response.data.d.results;
            }
            callback();
        });
    };




    return PrintService;

}]);