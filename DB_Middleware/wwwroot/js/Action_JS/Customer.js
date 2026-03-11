


var host = "https://localhost:44303/";
//Message popup defined here 
var returnvaliddivision = "Valid";
function createRoleSuccess(Tag, Message) {
    var title = "";
    var Tag1 = Tag;
    if (Tag1 == 'SUCCESS') {
            
        Tag = "success", title = Message
    }
    else if (Tag1 == 'error') {
        Tag = "error", title = Message
    }
    else if (Tag1 == 'info') {
        Tag = "info", title = Message
    }
    Swal.fire({
        icon: Tag,
        title: title,
        text: Message,
        timer: 55500,
        timerProgressBar: true,
        didOpen: () => {
            timerInterval = setInterval(() => {
                const content = Swal.getHtmlContainer()
                if (content) {
                    const b = content.querySelector('b')
                    if (b) {
                        b.textContent = Swal.getTimerLeft()
                    }
                }
            }, 10000)
        },
        willClose: () => {
            clearInterval(timerInterval)
        }
    }).then((result) => {
        if (result.dismiss === Swal.DismissReason.timer) {
            console.log('I was closed by the timer')
        }
    })


}

//*-------------------------------------------------Start segemnts section ----------------------------------*//
function Bind_dbSegmentsdetails() {
    $.ajax({
        /*    url: '@Url.Action("Bind_Compnay_Name", "BindMaster")',*/
        url: host + 'BindMaster/Bind_Segments',
        type: 'GET',
        data: {},
        contentType: "application/json; charset=utf-8",
        success: function (data) {


            var JsData = data;
            var binddata = '<option value="">Select</option>';
            for (var index = 0; index < JsData.length; index++) {
                binddata = binddata + '<option value="' + JsData[index].encriptid + '" >' + JsData[index].Segmentsname + '</option>';
            }
            $("#dbsegments").html(binddata);



        },
        failure: function (response) {
            alert(response.d);
        }
    });
}



function AddSegments() {
    $('#js-Segmentsmaster').validate({
        rules: {
            txtsegmentsname: "required",
            dbnbusinessgroup: "required",
            dbstatus: "required",

        },
        messages: {
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            // Add the `invalid-feedback` class to the error element
            error.addClass("invalid-feedback");
            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element.next(".pmd-textfield-focused"));
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },
        submitHandler: function (form) {
            //build json string
            var jsonstring = "";
            /*   $('.spinner').css('display', 'block').fadeIn();*/

            $.ajax({
                //url: '@Url.Action("ActionbySH", "Sales_Head")',
                url: host + 'Master/AddSegments',
                type: 'POST',
                data: {
                    Segmentsname: $("#txtsegmentsname").val(), businessgroup: $("#dbnbusinessgroup").val(), Status: $("#dbstatus").val(), Action: $("#btnsegments").text(), Segmentsid: $("#segementsId").val(),
                },
                success: function (data) {
                    var list = data;
                    if (list.Flag == 'SUCCESS') {
                        $("#btnsegments").text("Submit");
                        createRoleSuccess(list.Flag, list.sql_message);
                        Bind_Segmentsdetails_Details();
                        $("#txtsegmentsname").val('');
                        $("#dbnbusinessgroup").val('');
                        $("#dbstatus").val('');
                        $("#segementsId").val('');




                    }
                    else { createRoleSuccess(list.Flag, list.sql_message); /*$('.spinner').css('display', 'block').fadeOut(); */ }

                }
            });

        }
    })


}


function get_Segments_byid(editid) {
    $("#btnsegments").text("Update");
    $("#segementsId").val(editid);
    var businessgroupid = editid;

    $.ajax({
        type: "GET",
        url: host + 'BindMaster/Bind_Segments_Edit',

        data: { id: "" + businessgroupid + "" },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        crossDomain: true,
        success: function (Data) {
            $("#txtsegmentsname").val(Data.Segmentsname);
            $("#dbnbusinessgroup").val(Data.Businessgroup);
            $("#dbstatus").val(Data.Status);
            $("#segementsId").val(Data.encriptid);

        }
    });

}



function InactisegmentsMaster(segmentsid) {
    let confirmAction = confirm("Are you sure to execute this action?");
    if (confirmAction) {
        $.ajax({
            //url: '@Url.Action("ActionbySH", "Sales_Head")',
            url: host + 'Master/InactiveSegments',
            type: 'POST',
            data: { id: "" + segmentsid + "" },
            success: function (data) {
                var list = data;
                if (list.Flag == 'SUCCESS') {

                    createRoleSuccess(list.Flag, list.sql_message);
                    Bind_Segmentsdetails_Details();

                }
                else { createRoleSuccess(list.Flag, list.sql_message); /*$('.spinner').css('display', 'block').fadeOut(); */ }

            }
        });

    }
    else {

        alert("Action canceled");
    }
}

//*--------------------------------- End Segmebts Section  -----------------------------------------------------*//



//*---------------------------------------------Aspect Section-----------------------------------------------------*//
function Bind_dbAspect() {


    $.ajax({
        /*    url: '@Url.Action("Bind_Compnay_Name", "BindMaster")',*/
        url: host + 'BindMaster/Bind_Aspect',
        type: 'GET',
        data: {},
        contentType: "application/json; charset=utf-8",
        success: function (data) {


            var JsData = data;
            var binddata = '<option value="">Select</option>';
            for (var index = 0; index < JsData.length; index++) {
                binddata = binddata + '<option value="' + JsData[index].AspectName + '" >' + JsData[index].AspectName + '</option>';
            }
            $("#dbaspect").html(binddata);



        },
        failure: function (response) {
            alert(response.d);
        }
    });
}
function chekchighlighted() {
    var status = "Y";

    if ($("#dbaspect").val() == "" || $("#dbaspect").val() == null) {

        $("#dbaspect").addClass("is-invalid");
        status = "N";
    }
    else {

        $("#dbaspect").addClass("is-valid").removeClass("is-invalid");

    }
    if ($("#dbaspectcategory").val() == "" || $("#dbaspectcategory").val() == null) {

        $("#dbaspectcategory").addClass("is-invalid");
        status = "N";
    }
    else {

        $("#dbaspectcategory").addClass("is-valid").removeClass("is-invalid");

    }
    if ($("#dbconsiderations").val() == "" || $("#dbconsiderations").val() == null) {

        $("#dbconsiderations").addClass("is-invalid");
        status = "N";
    }
    else {

        $("#dbconsiderations").addClass("is-valid").removeClass("is-invalid");

    }
    return status;

}

function Bind_dbAspectcate(Aspect) {
    $.ajax({
        /*    url: '@Url.Action("Bind_Compnay_Name", "BindMaster")',*/
        url: host + 'BindMaster/Bind_AspectCategory',
        type: 'GET',
        data: { Aspect: "" + Aspect + "" },
        contentType: "application/json; charset=utf-8",
        success: function (data) {


            var JsData = data;
            var binddata = '<option value="">Select</option>';
            for (var index = 0; index < JsData.length; index++) {
                binddata = binddata + '<option value="' + JsData[index].Aspectcategory + '" >' + JsData[index].Aspectcategory + '</option>';
            }
            $("#dbaspectcategory").html(binddata);

            chekchighlighted();

        },
        failure: function (response) {
            alert(response.d);
        }
    });
}


function Bind_dbConsiderations(Aspectcategory) {
    $.ajax({
        /*    url: '@Url.Action("Bind_Compnay_Name", "BindMaster")',*/
        url: host + 'BindMaster/Bind_Considerations',
        type: 'GET',
        data: { Aspect: "" + $("#dbaspect").val() + "", AspectCate: "" + Aspectcategory + "" },
        contentType: "application/json; charset=utf-8",
        success: function (data) {


            var JsData = data;
            var binddata = '<option value="">Select</option>';
            for (var index = 0; index < JsData.length; index++) {
                binddata = binddata + '<option value="' + JsData[index].Considerations + '" >' + JsData[index].Considerations + '</option>';
            }
            $("#dbconsiderations").html(binddata);

            chekchighlighted();

        },
        failure: function (response) {
            alert(response.d);
        }
    });
}




//*------------------------------------------------End Aspect Section------------------------------------------------*//


//******************************************Add Draft Customer Details---------------------------------------------//
function uploadcategoryImg() {

    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());

    var rid = params['iid'];

    var data = new FormData();
    var files = "";
    var ftype = ""

    files = $("#customFileInput2").get(0).files;



    //var files = $("#customFileInput").get(0).files;


    if (files.length > 0) {
        data.append("HelpSectionImages", files[0]);
        data.append("requestid", rid);


    }

    var extension = $("#customFileInput2").val().split('.').pop().toUpperCase();

    if (files.length > 0) {

        if (extension != "PNG" && extension != "JPG" && extension != "GIF" && extension != "JPEG" && extension != "PDF" && extension != "TXT" && extension != "XLSX" && extension != "DOC" && extension != "DOCX") {
            alert("Please Upload Valid File");
            return false;
        }
        data.append("test", rid);
        data.append("test1", rid);
        $.ajax({
            url: host + 'Customer/uploadImg',
            type: "POST",
            processData: false,
            data: data,
            dataType: 'json',
            contentType: false,
            success: function (response) {
                imageuploadurl = response;
                alert(response);
            },
            error: function (er) { }

        });
        return false;
    }
}

function Save_Draft_Customer_Records() {

  
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['iid'];

    $('#js-customer_Save_Basic_Details').validate({ // initialize the plugin

        rules: {
            ignore: 'input[type=hidden], .select2-input, .select2-focusser',
            cname: "required",
            dbsegments: "required",

            dbofficelocation: "required",
            dbcustintrest: "required",
            txtprogramname: "required",
            txtexpliferevenue: "required",
            txtexpannualrevenue: "required",
            txtwinningcofidence: "required",
            txtsam: "required",
            dbsaletype: "required",
            dbcustgroup: "required",
            cshortname: "required",
            dbcusttype: "required",
            dbcustuse: "required",
            dbcountry: "required",
            dbzone: "required",
            txtremark: "required",
            txtpremark: "required",
            txtoaddress: "required",

            txtmobilenumber: {
                required: true,
                minlength: 10,
                maxlength: 10

            },
            emailid: {
                required: true,
                email: true
            }
        },
        messages: {

            txtmobilenumber: "Please enter a valid Mobile Number",
            emailid: "Please enter a valid Email",



        },
        errorElement: "em",

        errorPlacement: function (error, element) {
            // Add the `invalid-feedback` class to the error element
            error.addClass("invalid-feedback");
            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element.next(".pmd-textfield-focused"));
            }
        },
        highlight: function (element, errorClass, validClass) {

            if ($('#dbcustgroup').val() == '' || $('#dbcustgroup').val() == null) {

                $("#dbcustgroup").focus();
                $('#select1-dbcustgroup-container').parent().css('border-color', '#fd3995');
                validationcheck = "N";
            }
            else { $('#select1-dbcustgroup-container').parent().css('border-color', '#1dc9b7'); }


            if ($('#dbocountry').val() == '' || $('#dbocountry').val() == null) {

                $("#dbocountry").focus();
                $('#select2-dbocountry-container').parent().css('border-color', '#fd3995');
                validationcheck = "N";
            }
            else { $('#select2-dbocountry-container').parent().css('border-color', '#1dc9b7'); }

            if ($('#Odbcity').val() == '' || $('#Odbcity').val() == null) {

                $("#Odbcity").focus();
                $('#select2-Odbcity-container').parent().css('border-color', '#fd3995');

            }
            else { $('#select2-Odbcity-container').parent().css('border-color', '#1dc9b7'); }

            if ($('#dbpurcountry').val() == '' || $('#dbpurcountry').val() == null) {

                $("#dbpurcountry").focus();
                $('#select2-dbpurcountry-container').parent().css('border-color', '#fd3995');

            }
            else { $('#select2-dbpurcountry-container').parent().css('border-color', '#1dc9b7'); }

            if ($('#Ldbstate').val() == '' || $('#Ldbstate').val() == null) {

                $("#Ldbstate").focus();
                $('#select2-Ldbstate-container').parent().css('border-color', '#fd3995');

            }
            else { $('#select2-Ldbstate-container').parent().css('border-color', '#1dc9b7'); }



            if ($('#dbpurcity').val() == '' || $('#dbpurcity').val() == 'Null' || $('#dbpurcity').val() == null) {

                $("#dbpurcity").focus();
                $('#select2-dbpurcity-container').parent().css('border-color', '#fd3995');

            }
            else { $('#select2-dbpurcity-container').parent().css('border-color', '#1dc9b7'); }
            if ($('#dbproductname').val() == '') {
                $("#dbproductname").focus();
                $('#select2-dbproductname-container').parent().css('border-color', '#fd3995');


            }
            else { $('#select2-odbstate-container').parent().css('border-color', '#1dc9b7'); }

            if ($('#odbstate').val() == '' || $('#odbstate').val() == 'Null' || $('#odbstate').val() == null) {
                $("#odbstate").focus();
                $('#select2-odbstate-container').parent().css('border-color', '#fd3995');


            }
            else { $('#select2-odbstate-container').parent().css('border-color', '#1dc9b7'); }

            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },



        submitHandler: function (form) {

            const urlSearchParams = new URLSearchParams(window.location.search);
            const params = Object.fromEntries(urlSearchParams.entries());
            var rid = params['iid'];
            var validationcheck = "OK";
           
            if ($('#dbocountry').val() == '' || $('#dbocountry').val() == null) {

                $("#dbocountry").focus();
                $('#select2-dbocountry-container').parent().css('border-color', '#fd3995');
                validationcheck = "N";
            }
            else { $('#select2-dbocountry-container').parent().css('border-color', '#1dc9b7'); }

            if ($('#Odbcity').val() == '' || $('#Odbcity').val() == null) {

                $("#Odbcity").focus();
                $('#select2-Odbcity-container').parent().css('border-color', '#fd3995');
                validationcheck = "N";
            }
            else { $('#select2-Odbcity-container').parent().css('border-color', '#1dc9b7'); }

            if ($('#dbpurcountry').val() == '' || $('#dbpurcountry').val() == null) {

                $("#dbpurcountry").focus();
                $('#select2-dbpurcountry-container').parent().css('border-color', '#fd3995');
                validationcheck = "N";
            }
            else { $('#select2-dbpurcountry-container').parent().css('border-color', '#1dc9b7'); }

            if ($('#Ldbstate').val() == '' || $('#Ldbstate').val() == null) {

                $("#Ldbstate").focus();
                $('#select2-Ldbstate-container').parent().css('border-color', '#fd3995');
                validationcheck = "N";
            }
            else { $('#select2-Ldbstate-container').parent().css('border-color', '#1dc9b7'); }



            if ($('#dbpurcity').val() == '' || $('#dbpurcity').val() == 'Null' || $('#dbpurcity').val() == null) {

                $("#dbpurcity").focus();
                $('#select2-dbpurcity-container').parent().css('border-color', '#fd3995');
                validationcheck = "N";
            }
            else { $('#select2-dbpurcity-container').parent().css('border-color', '#1dc9b7'); }
            if ($('#dbproductname').val() == '') {
                $("#dbproductname").focus();
                $('#select2-dbproductname-container').parent().css('border-color', '#fd3995');
                validationcheck = "N";

            }
            else { $('#select2-dbproductname-container').parent().css('border-color', '#1dc9b7'); }
            if (validationcheck == "N") {

                alert("Please fill Required Field");
                return true;
            }

            

             
                //-------------------------------------------------------param-------------------------------


                //var cust = {};

                //cust.rid = rid;
                //cust.CustomerGroup = $("#dbcustgroup").val();
                //cust.Customer_name = $("#cname").val();
                //cust.cshortname = $("#cshortname").val();
                //cust.Customer_Type = $("#dbcusttype").val();
                //cust.Customer_Use = $("#dbcustuse").val();
                //cust.Business_Sale_type = $("#dbsaletype").val();
                //cust.Customer_Intrest = $("#dbcustintrest").val();
                //cust.Zone = $("#dbzone").val();
                //cust.OCountry = $("#dbocountry").val();
                //cust.OState = $("#odbstate").val();
                //cust.OCity = $("#Odbcity").val();
                //cust.OAddress = $("#txtoaddress").val();
                //cust.PCountry = $("#dbpurcountry").val();

                //cust.PState = $("#Ldbstate").val();
                //cust.PCity = $("#dbpurcity").val();
                //cust.PAddress = $("#txtpremark").val();
                //cust.Product_Name = $("#dbproductname").val();
                //cust.Program_Name = $("#txtprogramname").val();
                //cust.Segment = $("#dbsegments").val();


                //cust.Expected_Annual_Revenue = $("#txtexpannualrevenue").val();
                //cust.Expected_Lifetime_Revenue = $("#txtexpliferevenue").val();

                //if (document.getElementById('defaultInline1Radio13').checked) { cust.Business_In_LTS == "Yes" } else { cust.Business_In_LTS = "No"}
                //if (document.getElementById('defaultInline1Radio11').checked) { cust.Start_Up == "Yes" } else { cust.Start_Up = "No" }


                //cust.Winning_Confidence_Level = $("#txtwinningcofidence").val();
                //cust.SAM_CR = $("#txtsam").val();
                //cust.Action = $("#rollicd").text();



                //-------------------------------------------------------------------End Param-------------------------------------


                var data = new FormData();

                var ftype = ""

              



                //var files = $("#customFileInput").get(0).files;

       
                if (files.length > 0) {
                    data.append("HelpSectionImages", files[0]);
                    data.append("requestid", rid);


            }
               data.append("Exictingdoc", $("#hdnfiledoc").val());
                data.append("CustomerGroup", $("#dbcustgroup").val());
                data.append("Customer_Name", $("#cname").val());
                data.append("cshortname", $("#cshortname").val());
                data.append("Customer_Type", $("#dbcusttype").val());
                data.append("Customer_Use", $("#dbcustuse").val());
                data.append("Business_Sale_type", $("#dbsaletype").val());
                data.append("Customer_Intrest", $("#dbcustintrest").val());
                data.append("Zone", $("#dbzone").val());
                data.append("OCountry", $("#dbocountry").val());
                data.append("OState", $("#odbstate").val()); odbstate
                data.append("OCity", $("#Odbcity").val());
                data.append("OAddress", $("#txtoaddress").val());
                data.append("PCountry", $("#dbpurcountry").val());
                data.append("PState", $("#Ldbstate").val());
                data.append("PCity", $("#dbpurcity").val());
                data.append("PAddress", $("#txtpremark").val());
                data.append("Product_Name", $("#dbproductname").val());
                data.append("Program_Name", $("#txtprogramname").val());
                data.append("Segment", $("#dbsegments").val());
                data.append("Expected_Annual_Revenue", $("#txtexpannualrevenue").val());
                data.append("Expected_Lifetime_Revenue", $("#txtexpliferevenue").val());
             

                var Start_Up = "";
            var Business_In_LTS = "";
            var Backed_Promotor_by = "";
            
                if (document.getElementById('defaultInline1Radio11').checked) { Business_In_LTS = "Yes" } else { Business_In_LTS = "No" }
                if (document.getElementById('defaultInline1Radio13').checked) { Start_Up = "Yes" } else { Start_Up = "No" }


            data.append("Business_In_LTS", Business_In_LTS);
            data.append("Start_Up", Start_Up);
            if (Start_Up = "yes") {

                if (document.getElementById('defaultInline1Radio15').checked) { Backed_Promotor_by = "Backed" } else { Backed_Promotor_by = "Promotor by" }
            }
          
                data.append("Backed_Promotor_by", Backed_Promotor_by);
                data.append("Winning_Confidence_Level", $("#txtwinningcofidence").val());
                data.append("SAM_CR", $("#txtsam").val());
                data.append("Action", $("#rollicd").text());

                if (validationcheck != "N") {

                    $('.spinner').css('display', 'block').fadeIn();

                    $.ajax({
                        url: host + 'Customer/CustomerSave_onboarding_request',
                        type: 'POST',
                        processData: false,
                        data: data,
                        dataType: 'json',
                        contentType: false,
                        success: function (data) {
                            var list = data;

                            if (list.Flag == 'SUCCESS') {

                                createRoleSuccess(list.Flag, list.sql_message);
                                window.location.href = host + "Customer/Customer_Aspect?iid=" + list.Requestid + "";
                                $('.spinner').css('display', 'block').fadeOut();


                            }
                            else { createRoleSuccess(list.Flag, list.sql_message); $('.spinner').css('display', 'block').fadeOut(); }






                        }
                    });
                }
                else {
                    alert("Please fill Required Field");
                    return true;
                }

            }
        })


}


//******************************************************End  Draft Customer Details ------------------------------//

//***************************************************Check Duplicate Customer Name Exict ****************************//
function Chekc_Duplicate_Record() {

    $.ajax({
        /*    url: '@Url.Action("Bind_Compnay_Name", "BindMaster")',*/
        url: host + 'Customer/CheckDuplicate_Records',
        type: 'GET',
        data: { Customername: "" + $("#cname").val() + "" },
        contentType: "application/json; charset=utf-8",
        success: function (data) {


            var JsData = data;

            if (JsData.Status == "Yes") {

                Swal.fire(
                    {
                        position: "top-end",
                        type: "error",
                        title: "Oops...",
                        title: "Customer Name Already Exict?",
                        showConfirmButton: false,
                        timer: 1500
                    });

                $("#cname").val('');
                $("#cname").focus();
                return true;

            }

            else {

            }

        },
        failure: function (response) {
            alert(response.d);
        }
    });


}

//******************************************************End Duplicate Chekc customer Name *******************************//


//***********************Chekc valid search droupdown value ------------------------------------//

function checkvalidgreen() {
    if ($('#dbproductname').val() == '') {
        $("#dbproductname").focus();
        $('#select2-dbproductname-container').parent().css('border-color', '#fd3995');

    }
    else { $('#select2-dbproductname-container').parent().css('border-color', '#1dc9b7'); }


    if ($('#odbstate').val() == '') {
        $("#odbstate").focus();
        $('#select2-odbstate-container').parent().css('border-color', '#fd3995');

    }
    else { $('#select2-odbstate-container').parent().css('border-color', '#1dc9b7'); }

    if ($('#Odbcity').val() == '') {
        $("#Odbcity").focus();
        $('#select2-Odbcity-container').parent().css('border-color', '#fd3995');

    }
    else { $('#select2-Odbcity-container').parent().css('border-color', '#1dc9b7'); }

}
//++++++++++++++++++++++++++++++++++Edn Search droupdown value --------------------------------------//

//******************************************Add Draft Customer Details---------------------------------------------//



function Save_Customer_Division_Aspect_Details() {
    var returnvaliddivision = "Valid";
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params["iid"];

    $('#js-customeraspect').validate({ // initialize the plugin

        rules: {



        },
        messages: {



        },
        errorElement: "em",

        errorPlacement: function (error, element) {
            // Add the `invalid-feedback` class to the error element
            error.addClass("invalid-feedback");
            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element.next(".pmd-textfield-focused"));
            }
        },
        highlight: function (element, errorClass, validClass) {

            /* $(element).addClass("is-invalid").removeClass("is-valid");*/
        },
        unhighlight: function (element, errorClass, validClass) {
            /*$(element).addClass("is-valid").removeClass("is-invalid");*/
        },



        submitHandler: function (form) {

            //Loop through the Table rows and build a JSON array.
            var divisionmlist = new Array();

            if ($('#tbldivision').find('input[type=checkbox]:checked').length > 0) {
                $("#tbldivision tbody tr").each(function () {
                    var row = $(this);
                    var valList = {};

                    var Productinquiredid = "Productinquiredid" + row.find("TD").eq(0).html();
                    var CrossSellingID = "CrossSellingID" + row.find("TD").eq(0).html();
                    var remember = document.getElementById(Productinquiredid).checked;
                    var GetSellingID = document.getElementById(CrossSellingID).checked;
                    var TAMID = "#TAMID" + row.find("TD").eq(0).html();
                    var SAMID = "#SAMID" + row.find("TD").eq(0).html();
                    var SMWS = "#SMWS" + row.find("TD").eq(0).html();
                    var TargetedSobID = "#TargetedSobID" + row.find("TD").eq(0).html();
                    var TCID = "#TCID" + row.find("TD").eq(0).html();


                    if (remember == true) {

                        valList.Productinquiredid = "Yes";
                    }
                    else { valList.Productinquiredid = "No"; }
                    if (GetSellingID == true) {

                        valList.CrossSellingID = "Yes";
                    }
                    else { valList.CrossSellingID = "No"; }

                    valList.Division = row.find("TD").eq(1).html();


                    valList.srno = row.find("TD").eq(0).html();
                    if (remember == true || GetSellingID == true) {


                        if ($(TAMID).val() == "") {

                            $(TAMID).addClass("is-invalid");
                            returnvaliddivision = "Invalid";

                            $(TAMID).focus();
                            return true;
                        } else {
                            $(TAMID).removeClass("is-invalid");
                            valList.TAMID = $(TAMID).val();

                        }
                        if ($(SAMID).val() == "") {
                            $(SAMID).addClass("is-invalid");
                            returnvaliddivision = "Invalid";

                            $(SAMID).focus();
                            return true;
                        } else {
                            $(SAMID).removeClass("is-invalid");
                            valList.SAMID = $(SAMID).val();

                        }
                        if ($(SMWS).val() == "") {
                            $(SMWS).addClass("is-invalid");
                            returnvaliddivision = "Invalid";

                            $(SMWS).focus();
                            return true;
                        } else {
                            $(SMWS).removeClass("is-invalid");
                            valList.SMWS = $(SMWS).val();


                        }
                        if ($(TargetedSobID).val() == "") {
                            $(TargetedSobID).addClass("is-invalid");
                            returnvaliddivision = "Invalid";

                            $(TargetedSobID).focus();
                            return true;
                        } else {
                            $(TargetedSobID).removeClass("is-invalid");
                            valList.TargetedSobID = $(TargetedSobID).val();


                        }
                        if ($(TCID).val() == "") {
                            $(TCID).addClass("is-invalid");
                            returnvaliddivision = "Invalid";

                            $(TCID).focus();
                            return true;
                        } else {
                            $(TCID).removeClass("is-invalid");
                            valList.TCID = $(TCID).val();


                        }
                        valList.CustID = $("#custId").val();
                        divisionmlist.push(valList);


                    }


                });
            }

            else { alert('please select atleast one division'); return true; }
            if (($("#sumtotal").text()) > 60) { returnvaliddivision = "Valid"; } else { alert("This customer cannot be added as it is not meeting the desired criteria defined in the customer onboarding policy."); returnvaliddivision = "Invalid"; return true; }
            //Send the JSON array to Controller using AJAX.
          
            if (returnvaliddivision == "Invalid") { return true }


            if (returnvaliddivision == "Valid") {
                $('.spinner').css('display', 'block').fadeIn();
                $.ajax({
                    type: "POST",
                    url: "/Customer/Add_division_And_Aspect_Details",
                    /*   data: JSON.stringify(orders),*/
                    /* data: '{payload:"' + orders + '"}',*/

                    data: "{'Request':" + JSON.stringify(divisionmlist) + ",rid:'" + rid + "'}",

                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        Save_Aspect_Details();

                        /* alert(r + " record(s) inserted.");*/
                    }
                });
                $('.spinner').css('display', 'block').fadeOut();
            }
            //-------------------------------------------------------------------End Param-------------------------------------




        }
    })


}


function Save_Aspect_Details_Save_As_Draft() {
    //Add Aspect List----------------------------------------------------
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params["iid"];
    var returnvalidaspect = "Valid";
    var AspectList = new Array();
    $("#tblaspect tbody tr").each(function () {
        var row = $(this);
        var valList_Aspect = {};

        var AspectLid = "#AspectLid" + row.find("TD").eq(0).html();
        var AspectCateLid = "#AspectCateLid" + row.find("TD").eq(0).html();
        var Considerationid = "#Considerationid" + row.find("TD").eq(0).html();
        var txtrateid = "#txtrateid" + row.find("TD").eq(0).html();

        valList_Aspect.CustID = $("#custId").val();
        valList_Aspect.srno = row.find("TD").eq(0).html();
        if ($(AspectLid).val() == "") {
            $(AspectLid).addClass("is-invalid");
            returnvalidaspect = "Invalid";

            $(AspectLid).focus();
            return true;
        } else {
            $(AspectLid).removeClass("is-invalid");
            valList_Aspect.Aspectname = $(AspectLid).val();


        }
        if ($(AspectCateLid).val() == "") {
            $(AspectCateLid).addClass("is-invalid");
            returnvalidaspect = "Invalid";

            $(AspectCateLid).focus();
            return true;
        } else {
            $(AspectCateLid).removeClass("is-invalid");
            valList_Aspect.AspectCategory = $(AspectCateLid).val();


        }
        if ($(Considerationid).val() == "") {
            $(Considerationid).addClass("is-invalid");
            returnvalidaspect = "Invalid";
            $(Considerationid).focus();
            return true;
        } else {
            $(Considerationid).removeClass("is-invalid");
            valList_Aspect.Consideration = $(Considerationid).val();

        }
        if ($(txtrateid).val() == "") {
            $(txtrateid).addClass("is-invalid");
            returnvalidaspect = "Invalid";
            $(txtrateid).focus();
            return true;
        } else {
            $(txtrateid).removeClass("is-invalid");
            valList_Aspect.ratevalue = $(txtrateid).val();

        }


        AspectList.push(valList_Aspect);
    });
    if (returnvalidaspect == "Valid") {
        //End Aspect List//
        $('.spinner').css('display', 'block').fadeIn();
        $.ajax({

            url: host + 'Customer/Add_Aspect_Details_As_Save_Draft',
            type: 'Post',
            data: "{'Request':" + JSON.stringify(AspectList) + ",rid: '" + rid + "'}",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var list = data;

                createRoleSuccess(list.Flag, list.Message);
                if (list.Flag == "SUCCESS") {
                    window.location.href = host + "Customer/Bind_Customer";
                }

                $('.spinner').css('display', 'block').fadeOut();



            },
            failure: function (response) {
                alert(response.d);
            }
        });
    }
    else {


        alert("Please select required field");
    }

}

function Save_Aspect_Details() {
    //Add Aspect List----------------------------------------------------
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params["iid"];
    var returnvalidaspect = "Valid";
    var AspectList = new Array();
    $("#tblaspect tbody tr").each(function () {
        var row = $(this);
        var valList_Aspect = {};

        var AspectLid = "#AspectLid" + row.find("TD").eq(0).html();
        var AspectCateLid = "#AspectCateLid" + row.find("TD").eq(0).html();
        var Considerationid = "#Considerationid" + row.find("TD").eq(0).html();
        var txtrateid = "#txtrateid" + row.find("TD").eq(0).html();

        valList_Aspect.CustID = $("#custId").val();
        valList_Aspect.srno = row.find("TD").eq(0).html();
        if ($(AspectLid).val() == "") {
            $(AspectLid).addClass("is-invalid");
            returnvalidaspect = "Invalid";

            $(AspectLid).focus();
            return true;
        } else {
            $(AspectLid).removeClass("is-invalid");
            valList_Aspect.Aspectname = $(AspectLid).val();


        }
        if ($(AspectCateLid).val() == "") {
            $(AspectCateLid).addClass("is-invalid");
            returnvalidaspect = "Invalid";

            $(AspectCateLid).focus();
            return true;
        } else {
            $(AspectCateLid).removeClass("is-invalid");
            valList_Aspect.AspectCategory = $(AspectCateLid).val();


        }
        if ($(Considerationid).val() == "") {
            $(Considerationid).addClass("is-invalid");
            returnvalidaspect = "Invalid";
            $(Considerationid).focus();
            return true;
        } else {
            $(Considerationid).removeClass("is-invalid");
            valList_Aspect.Consideration = $(Considerationid).val();

        }
        if ($(txtrateid).val() == "") {
            $(txtrateid).addClass("is-invalid");
            returnvalidaspect = "Invalid";
            $(txtrateid).focus();
            return true;
        } else {
            $(txtrateid).removeClass("is-invalid");
            valList_Aspect.ratevalue = $(txtrateid).val();

        }


        AspectList.push(valList_Aspect);
    });
    if (returnvalidaspect == "Valid") {
        //End Aspect List//
        $('.spinner').css('display', 'block').fadeIn();
        $.ajax({

            url: host + 'Customer/Add_Aspect_Details',
            type: 'Post',
            data: "{'Request':" + JSON.stringify(AspectList) + ",rid: '" + rid + "'}",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var list = data;

                createRoleSuccess(list.Flag, list.Message);
                if (list.Flag == "SUCCESS") {
                    window.location.href = host + "Customer/Bind_Customer";
                }

                $('.spinner').css('display', 'block').fadeOut();



            },
            failure: function (response) {
                alert(response.d);
            }
        });
    }
    else {


        alert("Please select required field");
    }

}


//******************************************************End  Draft Customer Details ------------------------------//


//************************************Customer Details Save As Draft------------------------------------------------//

function Save_Customer_Division_Aspect_Details_Save_As_Draft() {
    var returnvaliddivision = "Valid";
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params["iid"];

    $('#js-customeraspect').validate({ // initialize the plugin

        rules: {



        },
        messages: {



        },
        errorElement: "em",

        errorPlacement: function (error, element) {
            // Add the `invalid-feedback` class to the error element
            error.addClass("invalid-feedback");
            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element.next(".pmd-textfield-focused"));
            }
        },
        highlight: function (element, errorClass, validClass) {

            /* $(element).addClass("is-invalid").removeClass("is-valid");*/
        },
        unhighlight: function (element, errorClass, validClass) {
            /*$(element).addClass("is-valid").removeClass("is-invalid");*/
        },



        submitHandler: function (form) {

            //Loop through the Table rows and build a JSON array.
            var divisionmlist = new Array();

            if ($('#tbldivision').find('input[type=checkbox]:checked').length > 0) {
                $("#tbldivision tbody tr").each(function () {
                    var row = $(this);
                    var valList = {};

                    var Productinquiredid = "Productinquiredid" + row.find("TD").eq(0).html();
                    var CrossSellingID = "CrossSellingID" + row.find("TD").eq(0).html();
                    var remember = document.getElementById(Productinquiredid).checked;
                    var GetSellingID = document.getElementById(CrossSellingID).checked;
                    var TAMID = "#TAMID" + row.find("TD").eq(0).html();
                    var SAMID = "#SAMID" + row.find("TD").eq(0).html();
                    var SMWS = "#SMWS" + row.find("TD").eq(0).html();
                    var TargetedSobID = "#TargetedSobID" + row.find("TD").eq(0).html();
                    var TCID = "#TCID" + row.find("TD").eq(0).html();


                    if (remember == true) {

                        valList.Productinquiredid = "Yes";
                    }
                    else { valList.Productinquiredid = "No"; }
                    if (GetSellingID == true) {

                        valList.CrossSellingID = "Yes";
                    }
                    else { valList.CrossSellingID = "No"; }

                    valList.Division = row.find("TD").eq(1).html();


                    valList.srno = row.find("TD").eq(0).html();
                    if (remember == true || GetSellingID == true) {


                        if ($(TAMID).val() == "") {

                            $(TAMID).addClass("is-invalid");
                            returnvaliddivision = "Invalid";

                            $(TAMID).focus();
                            return true;
                        } else {
                            $(TAMID).removeClass("is-invalid");
                            valList.TAMID = $(TAMID).val();

                        }
                        if ($(SAMID).val() == "") {
                            $(SAMID).addClass("is-invalid");
                            returnvaliddivision = "Invalid";

                            $(SAMID).focus();
                            return true;
                        } else {
                            $(SAMID).removeClass("is-invalid");
                            valList.SAMID = $(SAMID).val();

                        }
                        if ($(SMWS).val() == "") {
                            $(SMWS).addClass("is-invalid");
                            returnvaliddivision = "Invalid";

                            $(SMWS).focus();
                            return true;
                        } else {
                            $(SMWS).removeClass("is-invalid");
                            valList.SMWS = $(SMWS).val();


                        }
                        if ($(TargetedSobID).val() == "") {
                            $(TargetedSobID).addClass("is-invalid");
                            returnvaliddivision = "Invalid";

                            $(TargetedSobID).focus();
                            return true;
                        } else {
                            $(TargetedSobID).removeClass("is-invalid");
                            valList.TargetedSobID = $(TargetedSobID).val();


                        }
                        if ($(TCID).val() == "") {
                            $(TCID).addClass("is-invalid");
                            returnvaliddivision = "Invalid";

                            $(TCID).focus();
                            return true;
                        } else {
                            $(TCID).removeClass("is-invalid");
                            valList.TCID = $(TCID).val();


                        }
                        valList.CustID = $("#custId").val();
                        divisionmlist.push(valList);


                    }


                });
            }

            else { alert('please select atleast one division'); return true; }

            if (($("#sumtotal").text()) > 60) { returnvaliddivision = "Valid"; } else { alert("This customer cannot be added as it is not meeting the desired criteria defined in the customer onboarding policy."); returnvaliddivision = "Invalid"; return true; }
          //Send the JSON array to Controller using AJAX.
            if (returnvaliddivision == "Invalid") { return true }


            if (returnvaliddivision == "Valid") {
                $('.spinner').css('display', 'block').fadeIn();
                $.ajax({
                    type: "POST",
                    url: "/Customer/Add_division_And_Aspect_Details",
                    /*   data: JSON.stringify(orders),*/
                    /* data: '{payload:"' + orders + '"}',*/

                    data: "{'Request':" + JSON.stringify(divisionmlist) + ",rid:'" + rid + "'}",

                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        Save_Aspect_Details_Save_As_Draft();

                        /* alert(r + " record(s) inserted.");*/
                    }
                });
                $('.spinner').css('display', 'block').fadeOut();
            }
            //-------------------------------------------------------------------End Param-------------------------------------




        }
    })


}

//____________________________________End Customer Details Save As draft------------------------------------------//

function login() {

    $('#js-login').validate({ // initialize the plugin
        rules: {
            username: "required",
            passwprd: "required"

        },
        messages: {


        },
        errorElement: "em",

        errorPlacement: function (error, element) {
            // Add the `invalid-feedback` class to the error element
            error.addClass("invalid-feedback");
            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element.next(".pmd-textfield-focused"));
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },


        submitHandler: function (form) {


            $.ajax({

                url: host + 'Inter/Authentation',
                type: 'POST',
                data: { Username: $("#username").val(), Password: $("#password").val() },
                success: function (data) {
                    var list = data;

                    if (list.Flag == 'error') {
                        window.location.href = host + list.URL;


                    } else {
                        window.location.href = host + list.URL;

                    }



                }
            });

        }
    })
}


//_____________________________________________________Approvel Section ----------------------------------------------//



function ActionbyApprover(approveltype) {

    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    //alert(urlSearchParams);

    var rid = params['iid'];
    $('#js-Bind_approver').validate({ // initialize the plugin
        rules: {



            txtremark: "required"


        },
        messages: {



        },
        errorElement: "em",

        errorPlacement: function (error, element) {
            // Add the `invalid-feedback` class to the error element
            error.addClass("invalid-feedback");
            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element.next(".pmd-textfield-focused"));
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },



        submitHandler: function (form) {
            $('.spinner').css('display', 'block').fadeIn();

            //build json string
            var jsonstring = "";
            $.ajax({
                //url: '@Url.Action("ActionbySH", "Sales_Head")',
                url: host + 'Customer/ActionbyApproval',
                type: 'POST',
                data: {
                    Requestid: rid, Remark: $("#txtremark").val(), Typeapprovel: approveltype
                },
                success: function (data) {
                    var list = data;
                    createRoleSuccess(list.Flag, list.sql_message);
                    if (list.Flag == 'SUCCESS') {


                        window.location.href = host + "Customer/Approval_Details";

                        $('.spinner').css('display', 'block').fadeOut();
                    }
                    else { createRoleSuccess(list.Flag, list.sql_message); $('.spinner').css('display', 'block').fadeOut(); }






                }
            });

        }
    })


}

function logoutInitial() {

    $.ajax({

        url: host + 'Inter/Creatorsignout',
        success: function (data) {

            window.location.href = host;
        }
    });
}

//-------------------------------End Approvel Section Details -----------------------------------------------------//