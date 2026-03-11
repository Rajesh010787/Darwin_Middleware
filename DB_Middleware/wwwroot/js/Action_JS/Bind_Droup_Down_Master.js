
var host = "https://darwinmware.sparkminda.in/";
function Bind_dbtypeofobservation(editid) {

    $.ajax({
        /*    url: '@Url.Action("Bind_Compnay_Name", "BindMaster")',*/
        url:'/Loan/Bind_MasterDroupdown',
        type: 'GET',
        async: false,
        data: {typeid:3},

        contentType: "application/json; charset=utf-8",
        success: function (data) {


            var JsData = data;
            var binddata = '<option value="">Select</option>';
            for (var index = 0; index < JsData.length; index++) {


                binddata = binddata + '<option value="' + JsData[index].value + '" >' + JsData[index].name + '</option>';
            }
            $("#dbtypeofobservation").html(binddata);
            $("#dbtypeofobservation").val(editid);


        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function Bind_Costcenter(editcostcenter) {
    alert(editcostcenter);
    $.ajax({
        url: "/Loan/Bind_MasterDroupdown",
        type: "GET",
        data: { typeid: 1, Business_Unit: $("#txtbusinessvertical").val(), CostCenter: 'null', SapCostCenter: 'null', Profit_Center: 'null' },
        dataType: "json",
        async: false,
        success: function (data) {

            var binddata = '<option value="">Select</option>';

            for (var index = 0; index < data.length; index++) {
                binddata += '<option value="' + data[index].value + '">'
                    + data[index].name +
                    '</option>';
            }

            $("#dbcostcenter").html(binddata);
            $("#dbcostcenter").val(editcostcenter);

            
        }
    });
}
function Bind_SAPCostcenter(editsapcostcenter) {

    
    $.ajax({
        url: "/Loan/Bind_MasterDroupdown",
        type: "GET",
        data: { typeid: 2, Business_Unit: $("#txtbusinessvertical").val(), CostCenter: $("#dbcostcenter").val(), SapCostCenter: $("#dbsapcostcenter").val(), Profit_Center: $("#dbprfofitcenter").val() },
        dataType: "json",
        async: false,

        success: function (data) {

            var binddata = '<option value="">Select</option>';

            for (var index = 0; index < data.length; index++) {
                binddata += '<option value="' + data[index].value + '">'
                    + data[index].name +
                    '</option>';
            }

            $("#dbsapcostcenter").html(binddata);
            $("#dbsapcostcenter").val(editsapcostcenter);
        }
    });
}
function Bind_Profit_Center(editprofitcenter) {



    $.ajax({
        url: "/Loan/Bind_MasterDroupdown",
        type: "GET",
        data: { typeid: 3, Business_Unit: $("#txtbusinessvertical").val(), CostCenter: $("#dbcostcenter").val(), SapCostCenter: $("#dbsapcostcenter").val(), Profit_Center: $("#dbprfofitcenter").val() },
        dataType: "json",
        async: false,
        success: function (data) {

            var binddata = '<option value="">Select</option>';

            for (var index = 0; index < data.length; index++) {
                binddata += '<option value="' + data[index].value + '">'
                    + data[index].name +
                    '</option>';
            }

            $("#dbprfofitcenter").html(binddata);
            $("#dbprfofitcenter").val(editprofitcenter);
        }
    });
}
function Bind_Newcost_Center(editnewcostcenter) {



    $.ajax({
        url: "/Loan/Bind_MasterDroupdown",
        type: "GET",
        data: { typeid: 4, Business_Unit: $("#txtbusinessvertical").val(), CostCenter: $("#dbcostcenter").val(), SapCostCenter: $("#dbsapcostcenter").val(), Profit_Center: $("#dbprfofitcenter").val() },
        dataType: "json",
        async: false,
        success: function (data) {

            var binddata = '<option value="">Select</option>';

            for (var index = 0; index < data.length; index++) {
                binddata += '<option value="' + data[index].value + '">'
                    + data[index].name +
                    '</option>';
            }

            $("#dbnewcostcenter").html(binddata);
            $("#dbnewcostcenter").val(editnewcostcenter);
        }
    });
}
function Bind_BanckName() {



    $.ajax({
        url: "/Loan/Bind_MasterDroupdown",
        type: "GET",
        async: false,
        data: { typeid: 5, Business_Unit: $("#txtbusinessvertical").val(), CostCenter: $("#dbcostcenter").val(), SapCostCenter: $("#dbsapcostcenter").val(), Profit_Center: $("#dbprfofitcenter").val() },
        dataType: "json",
        success: function (data) {

            var binddata = '<option value="">Select</option>';

            for (var index = 0; index < data.length; index++) {
                binddata += '<option value="' + data[index].value + '">'
                    + data[index].name +
                    '</option>';
            }

            $("#dbbankname").html(binddata);
           
        }
    });
}
    function EditBind_BanckName(geteditdetails) {

  

        $.ajax({
            url: "/Loan/Bind_MasterDroupdown",
            type: "GET",

            data: { typeid: 5, Business_Unit: "null", CostCenter: "null", SapCostCenter: "null", Profit_Center: "null"},
            dataType: "json",
            success: function (data) {

                var binddata = '<option value="">Select</option>';

                for (var index = 0; index < data.length; index++) {
                    binddata += '<option value="' + data[index].value + '">'
                        + data[index].name +
                        '</option>';
                }

                $("#dbbankname").html(binddata);
                $("#dbbankname").val(geteditdetails);
                
            }
        });
}