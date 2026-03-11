var host = "https://darwinmware.sparkminda.in/";
//Message popup defined here 
function createRoleSuccess(Tag, Message, URL) {
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
        /*        title: title,*/
        text: Message,


    }).then(function () {
        if (Tag1 == 'SUCCESS') {

            window.location.href = URL;

        }
    });


}

// ... and by passing a parameter, you can execute something else for "Cancel".
 // ... and by passing a parameter, you can execute something else for "Cancel".
//------------------------------Email ID Request by HR in Pending  emp details --------------------------

function Save_EmailIDRequest() {
    var files = "";
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['id'];

    if ($("#dbpositiontype").val() == "Against Replacement Of Left Employee (OLD position)") {
        $("#txtreplaceecode").focus();
        $('#txtreplaceecode').addClass("is-invalid");
        validationcheck = "N";
    } else {
        $('#txtreplaceecode').parent().css('border-color', '#1dc9b7');
    }

    $('#EmailID_Request_BYHR').validate({
        rules: {
            dbchoosedomain: "required",
            dbchooseemptype: "required",
            dbpositiontype: "required",
            txtpositionid: "required",
            dbCriticalPosition: "required",
            txtremark: "required",
            txtconfrimjoiningdate: "required",
            txtproposedemailid: {
                required: true,
                email: true
            }
        },
        messages: {},
        errorElement: "em",
        errorPlacement: function (error, element) {
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


            var validationcheck = "OK";

            // Validation checks
            if ($("#dbpositiontype").val() == "Against Replacement Of Left Employee (OLD position)" && $("#txtreplaceecode").val() == '') {
                alert('Please insert employee code and click on search?');
                $("#txtreplaceecode").focus();
                $('#txtreplaceecode').addClass("is-invalid");
                validationcheck = "N";
                return false;  // Prevent form submission
            }

            if ($('#txtreplaceecode').val()!= '' && $('#txtrepemployeename').val() == '') {
                $("#txtreplaceecode").focus();
                $('#txtreplaceecode').addClass("is-invalid");
                alert('Please check employee ID');
                validationcheck = "N";
                return false;  // Prevent form submission
            } else {
                $('#txtreplaceecode').parent().css('border-color', '#1dc9b7');
            }

            if ($('#dbchooseemptype').val() == 'Temprarory') {
                if ($('#txttilldate').val() == "") {
                    $("#txttilldate").focus();
                    $('#txttilldate').addClass("is-invalid");
                    alert('Please select Till What Date');
                    validationcheck = "N";
                    return false;  // Prevent form submission
                
            } else {
                $('#txttilldate').parent().css('border-color', '#1dc9b7');
            }
        }

            if (($('#txtbusinessvertical').val() == 'SMIT') && ($('#txtdivision').val() == 'SMIT Pune')) {
                if ($('#dbsmit').val() == "") {
                    $("#dbsmit").focus();
                    $('#dbsmit').addClass("is-invalid");
                    alert('Please Select SMIT Sub Department');
                    validationcheck = "N";
                    return false;  // Prevent form submission}

                } else {
                    $('#dbsmit').parent().css('border-color', '#1dc9b7');
                }
            }
            if ($("#dbchoosedomain").val() == "mindasilca.sparkminda.in") {
                if ($('#mindasilikacommunicationemail').val() == '') {
                    alert('Please Insert Minda Silika Communication Email');
                    $("#mindasilikacommunicationemail").focus();
                    $('#mindasilikacommunicationemail').addClass("is-invalid");
                    validationcheck = "N";
                    return false;  // Prevent form submission
                } else {

                    $('#mindasilikacommunicationemail').parent().css('border-color', '#1dc9b7');
                }

            }
            if ($('#dbchoosegroup').val() == '') {
                alert('Please Select Group List');
                $("#dbchoosegroup").focus();
                $('#dbchoosegroup').addClass("is-invalid");
                validationcheck = "N";
                return false;  // Prevent form submission
            }

            var optionsgroupemail = $("#dbchoosegroup > option:selected");
            var Gethgroupemail = [];
            for (var optionsgroupemail of document.getElementById("dbchoosegroup").options) {
                if (optionsgroupemail.selected) {
                    Gethgroupemail.push(optionsgroupemail.value);
                }
            }

            var accessdomainandmeailidrequest = "";
            if (document.getElementById('defaultInline1Radio').checked) { accessdomainandmeailidrequest = "Only Domain ID" }
            if (document.getElementById('defaultInline2Radio').checked) { accessdomainandmeailidrequest = "For Internal Communication" }
            if (document.getElementById('defaultInline4Radio').checked) { accessdomainandmeailidrequest = "For Internal / External Communication" }
            // Show spinner and disable the submit button
            $('.spinner').css('display', 'block').fadeIn();
            $("#btnsubmithremailcreation").prop("disabled", true);  // Disable the button
            $("#btnsubmithremailcreation").text("Please Wait");
            var objdata = {
                UserUniqueID: rid,
                HR_Position_Type: $("#dbpositiontype").val(),
                HR_Proposed_Emailid: $("#txtproposedemailid").val(),
                HR_Chosse_Domain: $("#dbchoosedomain").val(),
                HR_Emp_Being_Replace_Ecode: $("#txtreplaceecode").val(),
                HR_Replace_Name: $("#txtrepemployeename").val(),
                HR_Replace_Designation: $("#txtrepdesignation").val(),
                HR_Replace_Grade: $("#txtrepgradelevel").val(),
                HR_Replace_Email: $("#txtexicitingemail").val(),
                HR_Employee_Type: $("#dbchooseemptype").val(),
                HR_Till_Date: $("#txttilldate").val(),
                SMITSubdepart: $("#dbsmit").val(),
                HR_JoiningConfirm_Date: $("#txtconfrimjoiningdate").val(),
                accessdomainandenmailidrequest: accessdomainandmeailidrequest,
                HR_Access_Required_Group: Gethgroupemail.toString().trim(),
                Position_ID: $("#txtpositionid").val(),
                Critical_Posotion: $("#dbCriticalPosition").val(),
                SilikaCEmail: $("#mindasilikacommunicationemail").val(),
                Remark: $("#txtremark").val()
            };
            $.ajax({
                url: '/Email_Request/Request_Email_Creation',
                type: 'POST',
                processData: false,
                async: false,
                data: JSON.stringify(objdata),
                dataType: 'json',
                contentType: "application/json;charset=utf-8",
                async: false,

                success: function (data) {
                    if (data.Flag == 'SUCCESS') {
                        createRoleSuccess(data.flag, data.message, "../Bind_Data_For_EmailCreation");
                        $('.spinner').fadeOut();
                    } else {
                        createRoleSuccess(data.flag, data.message, "/Email_Request/Bind_Data_For_EmailCreation");
                        $('.spinner').fadeOut();
                    }
                    // Re-enable the submit button after the request completes
                    $("#btnsubmit").prop("disabled", false);
                    $("#btnsubmit").text("Submit");
                },
                error: function () {
                    // In case of an error, re-enable the button as well
                    $("#btnsubmit").prop("disabled", false);
                    $("#btnsubmit").text("Submit");
                    $('.spinner').fadeOut();
                }
            });
        }
    });
}

//------------------------------------end email id bt HR in Pending emp details ----------------------------------

//------------------------------Confirm By HR for Not Joined Emp --------------------------

function Save_EmailIDNotRequest() {
    
    var files = "";
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['id'];
   


    if ($('#txtremark').val() == '') {

        $("#txtremark").focus();
        $('#txtremark').addClass("is-invalid");
        alert('Please insert remark');
       
        return true;
    } else { $('#txtremark').parent().css('border-color', '#1dc9b7'); }


            $('.spinner').css('display', 'block').fadeIn();

            $.ajax({
                url: '/Email_Request/Request_Email_Not_Joined',
                type: 'POST',
                processData: false,
                async: false,
                data: JSON.stringify(objdata),
                dataType: 'json',
                contentType: "application/json;charset=utf-8",
                async: false,

                success: function (data) {


                    if (data.Flag == 'SUCCESS') {

                        createRoleSuccess(data.flag, data.message, "../Bind_Data_For_EmailCreation");

                        $('.spinner').css('display', 'block').fadeOut();


                    }
                    else { createRoleSuccess(data.flag, data.message, "/Email_Request/Bind_Data_For_EmailCreation"); $('.spinner').css('display', 'block').fadeOut(); }






                }
            });


       
}
//------------------------------------end  ----------------------------------


//------------------------------Email By HR for Not Joined Emp --------------------------

function Save_EmailID_Request_By_Infrateam() {

    var files = "";
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['id'];
    var validation = "Y";

    if ($("#dbstatus").val() == "Approved") {

        if ($("#txtemailbyinfra").val() == "") {


            alert('Please insert Valid Email ID');
            $("#txtemailbyinfra").focus();
            $('#txtemailbyinfra').addClass("is-invalid");
            validation = "N";
            return true;



        }
        if ($("#txtpassword").val() == "") {


            alert('Please insert password');
            $("#txtpassword").focus();
            $('#txtpassword').addClass("is-invalid");
            validation = "N";
            return true;



        }
        if ($("#dbmslicenceallocated").val() == "") {
            alert('Please Select MS Licence Allocated');
            $("#dbmslicenceallocated").focus();
            $('#dbmslicenceallocated').addClass("is-invalid");
            validation = "N";
            return true;

        }
        if ($("#dbnewlicencerequestgeneradted").val() == "") {

            alert('Please Select New Licence Procurement Request Generated');
            $("#dbnewlicencerequestgeneradted").focus();
            $('#dbnewlicencerequestgeneradted').addClass("is-invalid");
            validation = "N";
            return true;
        }


    }


    if (validation == "N") {

        alert("Please Select Required Validation");
        return true;
    }


    $('#EmailID_Request_BYIT').validate({ // initialize the plugin

        rules: {
            txtemailbyinfra: {

                email: true
            },
            txtremark: "required",
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

            if ($("#dbstatus").val() == "Approved") {

                if ($("#txtemailbyinfra").val() == "") {

                    if ($("#txtemailbyinfra").val().match(/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/)) {
                        // valid email
                    } else {
                        alert('Please insert Valid Email ID');
                        $("#txtemailbyinfra").focus();
                        $('#txtemailbyinfra').addClass("is-invalid");
                        return true;
                    }


                }
                if ($("#txtpassword").val() == "") {
                    alert('Please Inserrt Password');
                    $("#txtpassword").focus();
                    $('#txtpassword').addClass("is-invalid");
                    return true;

                }
                if ($("#dbmslicenceallocated").val() == "") {
                    alert('Please Select MS Licence Allocated');
                    $("#dbmslicenceallocated").focus();
                    $('#dbmslicenceallocated').addClass("is-invalid");
                    return true;

                }
                if ($("#dbteamslicenseallocated").val() == "") {

                    alert('Please Select Team Allocated');
                    $("#dbteamslicenseallocated").focus();
                    $('#dbteamslicenseallocated').addClass("is-invalid");
                    return true;
                }
                if ($("#dbnewlicencerequestgeneradted").val() == "") {

                    alert('Please Select New Licence Procurement Request Generated');
                    $("#dbnewlicencerequestgeneradted").focus();
                    $('#dbnewlicencerequestgeneradted').addClass("is-invalid");
                    return true;
                }
              
           

            }
            $('.spinner').css('display', 'block').fadeIn();
            $("#btnsubmitemailinfra").prop("disabled", true);  // Disable the button
            $("#btnsubmitemailinfra").text("Please Wait");
            var objdata = {
                UserUniqueID: rid,
                ITEmailCreated: $("#txtemailbyinfra").val(),
                password: $("#txtpassword").val(),
                MS_LicenceAllocated: $("#dbmslicenceallocated").val(),
                NewLicence_Prucure: $("#dbnewlicencerequestgeneradted").val(),
                Remark: $("#txtremark").val(),
                Status: $("#dbstatus").val(),
                Teamstatus: $("#dbteamslicenseallocated").val()

            };


         

            $.ajax({
                url: '/Email_Request/Request_Validate_By_Infrateam',
                type: 'POST',
                processData: false,
                async: false,
                data: JSON.stringify(objdata),
                dataType: 'json',
                contentType: "application/json;charset=utf-8",
                async: false,

                success: function (data) {


                    if (data.Flag == 'SUCCESS') {

                        createRoleSuccess(data.flag, data.message, "../Bind_dataoninfra_EmailCreation");

                        $('.spinner').css('display', 'block').fadeOut();


                    }
                    else { createRoleSuccess(data.flag, data.message, "/Email_Request/Bind_dataoninfra_EmailCreation"); $('.spinner').css('display', 'block').fadeOut(); }






                }
            });


        }
    })


}
//------------------------------------end email id bt HR in Pending emp details ----------------------------------


//---------------------------------------Update Emaild ID IN Darwin Box ---------------------------------


function AddEmailiupdatein_Darwinbox() {
    // Ensure this function runs when the DOM is fully loaded

    $('#js-updateemailid').validate({
        rules: {
            txtemailid: {
                required: true,
                email: true // Optional: Validate the email format
            }
        },
        messages: {
            txtemailid: "Please enter a valid email address" // Example message
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
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
            // Optionally show a spinner or loading indicator
            $("#addicd").prop("disabled", true);  // Disable the button
            $("#addicd").text("Please Wait");
            var objdata = {
                EmailId: $("#txtemailid").val(),
                password: $("#txtpassword").val(),
                UniqueID: $("#txtuniqueid").text(),
                Fullname: $("#Firstname").val() + " " + $("#middlename").val() + " " + $("#lastname").val()
            };

          
            $.ajax({
                url: "/DB_View/Update_EmailONDB",
                type: 'POST',
                dataType: 'json', // Expecting JSON response
                contentType: 'application/json; charset=utf-8', // Sending JSON data

                //data: { UniqueID:$("#txtemailid").val(),EmailId:$("#txtemailid").val() },
                data: JSON.stringify(objdata),
                success: function (data) {


                    if (data.flag === 'SUCCESS') {
                        $('#modal-Tracking').modal('hide');
                        createRoleSuccess(data.flag, data.self_service_message,'');
                        // Handle success scenario
                        // e.g., display a success message or redirect
                    } else {
                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.self_service_message, '');
                        // $('.spinner').css('display', 'block').fadeOut();
                    }
                },
                error: function (xhr, status, error) {
                    // Handle AJAX error
                    console.error('AJAX Error:', status, error);
                    // Optionally hide the spinner
                    // $('.spinner').css('display', 'block').fadeOut();
                }
            });

        }
    });

}

//-------------------------End Email Update Section ----------------------------------------------------------------------------------------------------


//---------------------------------------confirm not joined ---------------------------------


function Update_By_HR_Emp_NotJoined() {
    // Ensure this function runs when the DOM is fully loaded
    var result = confirm("Are you sure this employee has not joined?");
    if (result == true) {
        $('#js-updatemnotjoined').validate({
            rules: {
                txtremark: "required",
            },
            messages: {
                txtemailid: "Please enter a valid email address" // Example message
            },
            errorElement: "em",
            errorPlacement: function (error, element) {
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
                // Optionally show a spinner or loading indicator
                // $('.spinner').css('display', 'block').fadeIn();
                var objdata = {
                    remark: $("#txtremark").val(),
                    UserUniqueID: $("#txtuniqueid").text()
                };

                $('.spinner').css('display', 'block').fadeIn();
                $.ajax({
                    url: "/DB_View/Updateempnotjoinded",
                    type: 'POST',
                    dataType: 'json', // Expecting JSON response
                    contentType: 'application/json; charset=utf-8', // Sending JSON data

                    //data: { UniqueID:$("#txtemailid").val(),EmailId:$("#txtemailid").val() },
                    data: JSON.stringify(objdata),
                    success: function (data) {


                        if (data.flag === 'SUCCESS') {
                            createRoleSuccess(data.flag, data.message,'/Email_Request/Bind_Data_For_EmailCreation');
                            // Handle success scenario
                            // e.g., display a success message or redirect
                        } else {
                            $('#modal-Tracking').modal('hide');
                            // Handle failure scenario
                            createRoleSuccess(data.flag, data.message,'/Email_Request/Bind_Data_For_EmailCreation');
                            // $('.spinner').css('display', 'block').fadeOut();
                        }
                    },
                    error: function (xhr, status, error) {
                        // Handle AJAX error
                        console.error('AJAX Error:', status, error);
                        // Optionally hide the spinner
                        // $('.spinner').css('display', 'block').fadeOut();
                    }
                });

            }
        });
    } else {
        return false;
    }
}

//-------------------------End not joined Section ----------------------------------------------------------------------------------------------------

//---------------------------------------Extend not joined ---------------------------------


function Update_By_HR_Emp_extenddoj() {
    // Ensure this function runs when the DOM is fully loaded
    var result = confirm("Are you sure want to extend joining date?");
    if (result == true) {
        $('#js-updateextenddoj').validate({
            rules: {
                txtextendremark: "required",
                txtconfrimjoiningdate: "required",
            },
            messages: {
                txtemailid: "Please enter a valid email address" // Example message
            },
            errorElement: "em",
            errorPlacement: function (error, element) {
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
                // Optionally show a spinner or loading indicator
                $('.spinner').css('display', 'block').fadeIn();
                var objdata = {
                    remark: $("#txtextendremark").val(),
                    UserUniqueID: $("#txtextenduniqueid").text(),
                    HR_JoiningConfirm_Date: $("#txtconfrimjoiningdate").val()
                };


                $.ajax({
                    url: "/Email_Request/Request_Email_Creation_Extend_DOJ",
                    type: 'POST',
                    dataType: 'json', // Expecting JSON response
                    contentType: 'application/json; charset=utf-8', // Sending JSON data

                    //data: { UniqueID:$("#txtemailid").val(),EmailId:$("#txtemailid").val() },
                    data: JSON.stringify(objdata),
                    success: function (data) {


                        if (data.flag === 'SUCCESS') {
                            $('#modal-Trackingnotjoining').modal('hide');
                            createRoleSuccess(data.flag, data.message, '/Email_Request/Bind_Data_For_EmailCreation');
                       
                            // Handle success scenario
                            // e.g., display a success message or redirect
                        } else {
                            $('#modal-Trackingnotjoining').modal('hide');
                            // Handle failure scenario
                            createRoleSuccess(data.flag, data.message, '/Email_Request/Bind_Data_For_EmailCreation');
                            // $('.spinner').css('display', 'block').fadeOut();
                        }
                    },
                    error: function (xhr, status, error) {
                        // Handle AJAX error
                        console.error('AJAX Error:', status, error);
                        // Optionally hide the spinner
                        // $('.spinner').css('display', 'block').fadeOut();
                    }
                });

            }
        });
    } else {
        return false;
    }
}

//-------------------------End Extend joined Section ----------------------------------------------------------------------------------------------------
function checkEmailfowarderemailid() {

  
    if ($("#txtmailfowarderemailid").val()!= "") {
        var filter = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!filter.test($("#txtmailfowarderemailid").val())) {
            alert('Please provide a valid email address');
            $("#txtmailfowarderemailid").focus();
            $("#txtmailfowarderemailid").addClass("is-invalid").removeClass("is-valid");


            return true;
        }
        else {

            $("#txtmailfowarderemailid").addClass("is-valid").removeClass("is-invalid");

        }
    }
}
function checkEmailfowarderemailidbackup() {
  
    if ($("#txtmailfowarderemailidbackup").val() != "") {
        var filter = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!filter.test($("#txtmailfowarderemailidbackup").val())) {
            alert('Please provide a valid email address');

            $("#txtmailfowarderemailidbackup").addClass("is-invalid").removeClass("is-valid");

            $("#txtmailfowarderemailidbackup").focus();
            return true;
        } else {

            $("#txtmailfowarderemailidbackup").addClass("is-valid").removeClass("is-invalid");

        }
    }
}

//---------------------------------------Validate_By_RM_ForRemove_EmailID ---------------------------------


function Validate_By_RM_ForRemove_EmailID() {
    // Ensure this function runs when the DOM is fully loaded
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['id'];
    $('#Email_Remove_Validate_By_RM').validate({

        messages: {
            txtmailfowarderemailid: "Please enter a valid email address" // Example message
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element.next(".pmd-textfield-focused"));
            }
        },
        //highlight: function (element, errorClass, validClass) {
        //    $(element).addClass("is-invalid").removeClass("is-valid");
        //},
        //unhighlight: function (element, errorClass, validClass) {
        //    $(element).addClass("is-valid").removeClass("is-invalid");
        //},
        submitHandler: function (form) {
            // Optionally show a spinner or loading indicator
            // $('.spinner').css('display', 'block').fadeIn();
            var Fwdr_Email_Status = "";
            var GivenEmail_Onedrive = "";
            if (document.getElementById('customRadio1').checked) { Fwdr_Email_Status = "Y" } else if (document.getElementById('customRadio2').checked) { Fwdr_Email_Status = "N" }
            if (document.getElementById('customRadio3').checked) { GivenEmail_Onedrive = "Y" } else if (document.getElementById('customRadio4').checked) { GivenEmail_Onedrive = "N" }

            if ((Fwdr_Email_Status == "Y") && ($("#txtmailfowarderemailid").val() == '')) {

                alert("Please Insert Mail forwarding Email ID");
                $("#txtmailfowarderemailid").focus();
                return true;
            }
            if ((GivenEmail_Onedrive == "Y") && ($("#txtmailfowarderemailidbackup").val() == '')) {

                alert("Please Insert Whome to be given Email / Onedrive Backup");
                $("#txtmailfowarderemailidbackup").focus();
                return true;
            }
            if ($("#txtmailfowarderemailid").val() != "") {
                var filter = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                if (!filter.test($("#txtmailfowarderemailid").val())) {
                    alert('Please provide a valid email address');
                    $("#txtmailfowarderemailid").focus();
                    $("#txtmailfowarderemailid").addClass("is-invalid").removeClass("is-valid");


                    return true;
                }
                else {
                   
                    $("#txtmailfowarderemailid").addClass("is-valid").removeClass("is-invalid");

                }
            }

            if ($("#txtmailfowarderemailidbackup").val()!= "") {
                var filter = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                if (!filter.test($("#txtmailfowarderemailidbackup").val())) {
                    alert('Please provide a valid email address');

                    $("#txtmailfowarderemailidbackup").addClass("is-invalid").removeClass("is-valid");

                    $("#txtmailfowarderemailidbackup").focus();
                    return true;
                } else {
                  
                    $("#txtmailfowarderemailidbackup").addClass("is-valid").removeClass("is-invalid");

                }
            }
            if ($("#txtremark").val() == "") {

                alert('Please Insert Remark Details?');
                $("#txtremark").focus();

                return true;
            }
            var objdata = {
                Fwdr_Email_Description: $("#txtmailfowarderemailid").val(),
                GivenEmail_Onedrive_des: $("#txtmailfowarderemailidbackup").val(),
                Fwdr_Email_Status: Fwdr_Email_Status,
                GivenEmail_Onedrive: GivenEmail_Onedrive,
                Remark: $("#txtremark").val(),

                UniquieID: $("#txtuniqueid").text()
            };


            $.ajax({
                url: "/Inactive_Emp_Details/Validate_RM_Remove_Email",
                type: 'POST',
                dataType: 'json', // Expecting JSON response
                contentType: 'application/json; charset=utf-8', // Sending JSON data

                //data: { UniqueID:$("#txtemailid").val(),EmailId:$("#txtemailid").val() },
                data: JSON.stringify(objdata),
                success: function (data) {


                    if (data.flag === 'SUCCESS') {

                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Inactive_Emp_Details/Bind_Inactive_Emp");
                        // Handle success scenario
                        // e.g., display a success message or redirect
                    } else {
                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Inactive_Emp_Details/Bind_Inactive_Emp");
                        // $('.spinner').css('display', 'block').fadeOut();
                    }
                },
                error: function (xhr, status, error) {
                    // Handle AJAX error
                    console.error('AJAX Error:', status, error);
                    // Optionally hide the spinner
                    // $('.spinner').css('display', 'block').fadeOut();
                }
            });

        }
    });

}

//-------------------------End Validate_By_RM_ForRemove_EmailID ----------------------------------------------------------------------------------------------------




//---------------------------------------Validate_By_Local_ForRemove_EmailID ---------------------------------


function Email_Remove_Validate_By_Local_IT() {
    // Ensure this function runs when the DOM is fully loaded
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['id'];
    $('#Email_Remove_Validate_By_Local_IT').validate({

        rules: {
            txtpsdlocation: "required",
            dbpsddonalodstatus: "required",
          
            txtremark: "required",
        },
        messages: {
            txtemailid: "Please enter a valid email address" // Example message
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
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
       
            var objdata = {
                pststatus: $("#dbpsddonalodstatus").val(),
                pstlocation: $("#txtpsdlocation").val(),
                status: $("#dbstatus").val(),
                Remark: $("#txtremark").val(),

                UniquieID: $("#txtuniqueid").text()
            };


            $('.spinner').css('display', 'block').fadeIn();


            $("#addicdlocalit").prop("disabled", true);  // Disable the button
            $("#addicdlocalit").text("Please Wait");
            $.ajax({
                url: "/Inactive_Emp_Details/Validate_LocalIT_Remove_Email",
                type: 'POST',
                dataType: 'json', // Expecting JSON response
                contentType: 'application/json; charset=utf-8', // Sending JSON data

                //data: { UniqueID:$("#txtemailid").val(),EmailId:$("#txtemailid").val() },
                data: JSON.stringify(objdata),
                success: function (data) {


                    if (data.flag === 'SUCCESS') {

                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Inactive_Emp_Details/Bind_Inactive_Emp_Local_IT");
                        // Handle success scenario
                        // e.g., display a success message or redirect
                    } else {
                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Inactive_Emp_Details/Bind_Inactive_Emp_Local_IT");
                        // $('.spinner').css('display', 'block').fadeOut();
                    }
                },
                error: function (xhr, status, error) {
                    // Handle AJAX error
                    console.error('AJAX Error:', status, error);
                    // Optionally hide the spinner
                    // $('.spinner').css('display', 'block').fadeOut();
                }
            });

        }
    });

}

//-------------------------End Validate_By_RM_ForRemove_EmailID ----------------------------------------------------------------------------------------------------

//---------------------------------------Validate_By_Infrateam_ForRemove_EmailID ---------------------------------


function Validate_By_Infrateam_ForRemove_EmailID() {
    // Ensure this function runs when the DOM is fully loaded
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['id'];
    $('#js-emailremove_updatebyinfra').validate({
        rules: {
            dbpsddonalodstatus: "required",
          
            dbstatus: "required",
               txtremark: "required"
            
        },
        messages: {
           
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
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
            // Optionally show a spinner or loading indicator
            // $('.spinner').css('display', 'block').fadeIn();
           
          
            var objdata = {
                pststatus: $("#dbpsddonalodstatus").val(),
                pstlocation: $("#txtpsdlocation").val(),
                status: $("#dbstatus").val(),
                Remark: $("#txtremark").val(),

                UniquieID: $("#txtuniqueid").text()
            };
            $('.spinner').css('display', 'block').fadeIn();

            $.ajax({
                url: "/Inactive_Emp_Details/Validate_Infra_Remove_Email",
                type: 'POST',
                dataType: 'json', // Expecting JSON response
                contentType: 'application/json; charset=utf-8', // Sending JSON data

                //data: { UniqueID:$("#txtemailid").val(),EmailId:$("#txtemailid").val() },
                data: JSON.stringify(objdata),
                success: function (data) {


                    if (data.flag === 'SUCCESS') {

                        $('#modal-Trackinginactive_infrateam').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Inactive_Emp_Details/Bind_Inactive_Emp_oninfra");
                        // Handle success scenario
                        // e.g., display a success message or redirect
                    } else {
                        $('#modal-Trackinginactive_infrateam').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Inactive_Emp_Details/Bind_Inactive_Emp_oninfra");
                        // $('.spinner').css('display', 'block').fadeOut();
                    }
                },
                error: function (xhr, status, error) {
                    // Handle AJAX error
                    console.error('AJAX Error:', status, error);
                    // Optionally hide the spinner
                    // $('.spinner').css('display', 'block').fadeOut();
                }
            });

        }
    });

}

//-------------------------Validate_By_Infrateam_ForRemove_EmailID----------------------------------------------------------------------------------------------------



//---------------------------------------Validate_By_HR_ForBlockID ---------------------------------


function Validate_By_HR_ForBlockID() {
    // Ensure this function runs when the DOM is fully loaded
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['id'];
    $('#Email_Block_Validate_By_HR').validate({

        messages: {
          
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element.next(".pmd-textfield-focused"));
            }
        },
        //highlight: function (element, errorClass, validClass) {
        //    $(element).addClass("is-invalid").removeClass("is-valid");
        //},
        //unhighlight: function (element, errorClass, validClass) {
        //    $(element).addClass("is-valid").removeClass("is-invalid");
        //},
        submitHandler: function (form) {
            // Optionally show a spinner or loading indicator
            // $('.spinner').css('display', 'block').fadeIn();
            $('.spinner').css('display', 'block').fadeIn();
            if ($("#txtremark").val() == "") {

                alert('Please Insert Remark Details?');
                $("#txtremark").focus();

                return true;
            }
            var objdata = {
               
                Remark: $("#txtremark").val(),

                UniquieID: $("#txtuniqueid").text()
            };

            $('.spinner').css('display', 'block').fadeIn();
            $("#addicdhrblock").prop("disabled", true);  // Disable the button
            $("#addicdhrblock").text("Please Wait");
            $.ajax({
                url: "/Block_Email_Request/Validate_HR_Block_Email",
                type: 'POST',
                dataType: 'json', // Expecting JSON response
                contentType: 'application/json; charset=utf-8', // Sending JSON data

                //data: { UniqueID:$("#txtemailid").val(),EmailId:$("#txtemailid").val() },
                data: JSON.stringify(objdata),
                success: function (data) {


                    if (data.flag === 'SUCCESS') {

                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Block_Email_Request/Bind_Email_Block_Request_Details");
                        // Handle success scenario
                        // e.g., display a success message or redirect
                    } else {
                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Block_Email_Request/Bind_Email_Block_Request_Details");
                        // $('.spinner').css('display', 'block').fadeOut();
                    }
                },
                error: function (xhr, status, error) {
                    // Handle AJAX error
                    console.error('AJAX Error:', status, error);
                    // Optionally hide the spinner
                    // $('.spinner').css('display', 'block').fadeOut();
                }
            });

        }
    });

}

//-------------------------End Validate_By_RM_ForRemove_EmailID ----------------------------------------------------------------------------------------------------



//---------------------------------------Validate_By_RM_ForBlock_EmailID ---------------------------------


function Validate_By_RM_ForBlock_EmailID() {
    // Ensure this function runs when the DOM is fully loaded
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['id'];
    $('#Email_Block_Validate_By_RM').validate({

        messages: {
            txtmailfowarderemailid: "Please enter a valid email address" // Example message
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element.next(".pmd-textfield-focused"));
            }
        },
        //highlight: function (element, errorClass, validClass) {
        //    $(element).addClass("is-invalid").removeClass("is-valid");
        //},
        //unhighlight: function (element, errorClass, validClass) {
        //    $(element).addClass("is-valid").removeClass("is-invalid");
        //},
        submitHandler: function (form) {
            // Optionally show a spinner or loading indicator
            // $('.spinner').css('display', 'block').fadeIn();
            var Fwdr_Email_Status = "";
            var GivenEmail_Onedrive = "";
            if (document.getElementById('customRadio1').checked) { Fwdr_Email_Status = "Y" } else if (document.getElementById('customRadio2').checked) { Fwdr_Email_Status = "N" }
            if (document.getElementById('customRadio3').checked) { GivenEmail_Onedrive = "Y" } else if (document.getElementById('customRadio4').checked) { GivenEmail_Onedrive = "N" }

            if ((Fwdr_Email_Status == "Y") && ($("#txtmailfowarderemailid").val() == '')) {

                alert("Please Insert Mail forwarding Email ID");
                $("#txtmailfowarderemailid").focus();
                return true;
            }
            if ((GivenEmail_Onedrive == "Y") && ($("#txtmailfowarderemailidbackup").val() == '')) {

                alert("Please Insert Whome to be given Email / Onedrive Backup");
                $("#txtmailfowarderemailidbackup").focus();
                return true;
            }
            if ($("#txtmailfowarderemailid").val() != "") {
                var filter = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                if (!filter.test($("#txtmailfowarderemailid").val())) {
                    alert('Please provide a valid email address');
                    $("#txtmailfowarderemailid").focus();
                    $("#txtmailfowarderemailid").addClass("is-invalid").removeClass("is-valid");


                    return true;
                }
                else {

                    $("#txtmailfowarderemailid").addClass("is-valid").removeClass("is-invalid");

                }
            }

            if ($("#txtmailfowarderemailidbackup").val() != "") {
                var filter = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                if (!filter.test($("#txtmailfowarderemailidbackup").val())) {
                    alert('Please provide a valid email address');

                    $("#txtmailfowarderemailidbackup").addClass("is-invalid").removeClass("is-valid");

                    $("#txtmailfowarderemailidbackup").focus();
                    return true;
                } else {

                    $("#txtmailfowarderemailidbackup").addClass("is-valid").removeClass("is-invalid");

                }
            }
            if ($("#txtremark").val() == "") {

                alert('Please Insert Remark Details?');
                $("#txtremark").focus();

                return true;
            }
            var objdata = {
                Fwdr_Email_Description: $("#txtmailfowarderemailid").val(),
                GivenEmail_Onedrive_des: $("#txtmailfowarderemailidbackup").val(),
                Fwdr_Email_Status: Fwdr_Email_Status,
                GivenEmail_Onedrive: GivenEmail_Onedrive,
                Remark: $("#txtremark").val(),

                UniquieID: $("#txtuniqueid").text()
            };
            $('.spinner').css('display', 'block').fadeIn();
    
            $("#addicdrmblock").prop("disabled", true);  // Disable the button
            $("#addicdrmblock").text("Please Wait");
            $.ajax({
                url: "/Block_Email_Request/Validate_RM_Block_Email",
                type: 'POST',
                dataType: 'json', // Expecting JSON response
                contentType: 'application/json; charset=utf-8', // Sending JSON data

                //data: { UniqueID:$("#txtemailid").val(),EmailId:$("#txtemailid").val() },
                data: JSON.stringify(objdata),
                success: function (data) {


                    if (data.flag === 'SUCCESS') {

                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Block_Email_Request/Bind_Email_Block_RM");
                        $('.spinner').css('display', 'block').fadeOut();
                        // Handle success scenario
                        // e.g., display a success message or redirect
                    } else {
                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Block_Email_Request/Bind_Email_Block_RM");
                        $('.spinner').css('display', 'block').fadeOut();
                    }
                },
                error: function (xhr, status, error) {
                    // Handle AJAX error
                    console.error('AJAX Error:', status, error);
                    // Optionally hide the spinner
                    // $('.spinner').css('display', 'block').fadeOut();
                }
            });

        }
    });

}

//-------------------------End Validate_By_RM_ForBlock_EmailID ----------------------------------------------------------------------------------------------------






//---------------------------------------Validate_By_Infrateam_ForBlockEmailID ---------------------------------


function Validate_By_Infrateam_ForBlock_EmailID() {
    // Ensure this function runs when the DOM is fully loaded
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['id'];
    $('#js-emailblock_updatebyinfra').validate({
        rules: {
           
            dbstatus: "required",
            txtremark: "required"

        },
        messages: {

        },
        errorElement: "em",
        errorPlacement: function (error, element) {
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
            // Optionally show a spinner or loading indicator
            // $('.spinner').css('display', 'block').fadeIn();


            var objdata = {
                pststatus: '',
                pstlocation:'',
                status: $("#dbstatus").val(),
                Remark: $("#txtremark").val(),

                UniquieID: $("#txtuniqueid").text()
            };

            $('.spinner').css('display', 'block').fadeIn();
            $("#btnsubmitblockinfra").prop("disabled", true);  // Disable the button
            $("#btnsubmitblockinfra").text("Please Wait");
            $.ajax({
                url: "/Block_Email_Request/Validate_Infra_Block_Email",
                type: 'POST',
                dataType: 'json', // Expecting JSON response
                contentType: 'application/json; charset=utf-8', // Sending JSON data

                //data: { UniqueID:$("#txtemailid").val(),EmailId:$("#txtemailid").val() },
                data: JSON.stringify(objdata),
                success: function (data) {


                    if (data.flag === 'SUCCESS') {

                        $('#modal-Tracking_infrateam').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Block_Email_Request/Bind_Email_Block_on_Infra");
                        // Handle success scenario
                        // e.g., display a success message or redirect
                    } else {
                        $('#modal-Tracking_infrateam').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Block_Email_Request/Bind_Email_Block_on_Infra");
                        // $('.spinner').css('display', 'block').fadeOut();
                    }
                },
                error: function (xhr, status, error) {
                    // Handle AJAX error
                    console.error('AJAX Error:', status, error);
                    // Optionally hide the spinner
                    // $('.spinner').css('display', 'block').fadeOut();
                }
            });

        }
    });

}

//-------------------------Validate_By_Infrateam_ForBlock_EmailID----------------------------------------------------------------------------------------------------


//---------------------------------------Validate_By_HR_ForInternal Transfer ---------------------------------


function Validate_By_HR_ForTranfer() {
    // Ensure this function runs when the DOM is fully loaded
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['id'];
    $('#HR_Update_By_Internal_Tranfer').validate({
        rules: {
           
            txtremark: "required",
            tsteffectivedate: "required"
        },
        messages: {

        },
        errorElement: "em",
        errorPlacement: function (error, element) {
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
            // Optionally show a spinner or loading indicator
      
            if ($('#dbchoosegroup').val() == '') {
                alert('Please Sleect of Group List');
                $("#dbchoosegroup").focus();
                $('#dbchoosegroup').addClass("is-invalid");
                validationcheck = "N";
                return true;
            }
            var optionsgroupemail = $("#dbchoosegroup > option:selected");
            var Gethgroupemail = [];
            for (var optionsgroupemail of document.getElementById("dbchoosegroup").options) {
                if (optionsgroupemail.selected) {
                    Gethgroupemail.push(optionsgroupemail.value);
                }
            }
            var accessdomainandmeailidrequest = "";


            if (document.getElementById('defaultInline1Radio').checked) { accessdomainandmeailidrequest = "Only Domain ID" }
            if (document.getElementById('defaultInline2Radio').checked) { accessdomainandmeailidrequest = "For Internal Communication" }
            if (document.getElementById('defaultInline4Radio').checked) { accessdomainandmeailidrequest = "For Internal / External Communication" }

            var objdata = {

                Remark: $("#txtremark").val(),

                UserUniqueID: rid,

                accessdomainandenmailidrequest:accessdomainandmeailidrequest,
                Effectivedate: $("#tsteffectivedate").val(),
                HR_Access_Required_Group: Gethgroupemail.toString().trim()
            };
        
            $('.spinner').css('display', 'block').fadeIn();
            $("#btnsubmithrtranfer").prop("disabled", true);  // Disable the button
            $("#btnsubmithrtranfer").text("Please Wait");
            $.ajax({
                url: "/Internal_Transfer/VAlidate_By_HR_Transfercase",
                type: 'POST',
                processData: false,
                async: false,
                contentType: 'application/json; charset=utf-8', // Sending JSON data
                data:JSON.stringify(objdata),
                success: function (data) {


                    if (data.flag === 'SUCCESS') {

                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Internal_Transfer/Bind_On_HR");
                        // Handle success scenario
                        // e.g., display a success message or redirect
                    } else {
                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Internal_Transfer/Bind_On_HR");
                        // $('.spinner').css('display', 'block').fadeOut();
                    }
                },
                error: function (xhr, status, error) {
                    // Handle AJAX error
                    console.error('AJAX Error:', status, error);
                    // Optionally hide the spinner
                    // $('.spinner').css('display', 'block').fadeOut();
                }
            });

        }
    });

}

//-------------------------End Validate_By_HR Internal Transfer----------------------------------------------------------------------------------------------------



//---------------------------------------Validate_By_HR_ForDirectInternal Transfer ---------------------------------


function Infra_Update_By_DirectInternal_Tranfer() {
    // Ensure this function runs when the DOM is fully loaded
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['id'];
    $('#Infra_Update_By_DirectInternal_Tranfer').validate({
        rules: {

            txtremark: "required",
           
        },
        messages: {

        },
        errorElement: "em",
        errorPlacement: function (error, element) {
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
            // Optionally show a spinner or loading indicator

          

            var objdata = {

                Remark: $("#txtremark").val(),

                UserUniqueID: rid,

              
            };

            $('.spinner').css('display', 'block').fadeIn();
            $("#btnsubmithrtranfer").prop("disabled", true);  // Disable the button
            $("#btnsubmithrtranfer").text("Please Wait");
            $.ajax({
                url: "/Internal_Transfer/VAlidate_By_Infra_DirectTransfercase",
                type: 'POST',
                processData: false,
                async: false,
                contentType: 'application/json; charset=utf-8', // Sending JSON data
                data: JSON.stringify(objdata),
                success: function (data) {


                    if (data.flag === 'SUCCESS') {

                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Internal_Transfer/Bind_Direct_on_infra");
                        // Handle success scenario
                        // e.g., display a success message or redirect
                    } else {
                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Internal_Transfer/Bind_Direct_on_infra");
                        // $('.spinner').css('display', 'block').fadeOut();
                    }
                },
                error: function (xhr, status, error) {
                    // Handle AJAX error
                    console.error('AJAX Error:', status, error);
                    // Optionally hide the spinner
                    // $('.spinner').css('display', 'block').fadeOut();
                }
            });

        }
    });

}

//-------------------------End Validate_By_HR_ForDirectInternal  Transfer----------------------------------------------------------------------------------------------------


//---------------------------------------Validate_By_INRA_ForInternal Transfer ---------------------------------


function Validate_By_Infra_ForTranfer() {
    // Ensure this function runs when the DOM is fully loaded


    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['id'];
    $('#EmailID_Request_BYIT').validate({
        rules: {

            txtremarkinfra: "required",
           
            dbstatus: "required",

        },
        messages: {

        },
        errorElement: "em",
        errorPlacement: function (error, element) {
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
            // Optionally show a spinner or loading indicator
          
            if ($('#dbstatus').val() == 'Approved') {
                if ($("#dbmslicenceallocated").val() == '') {
                    alert('Please Sleect of MS Licence Allocated');
                    $("#dbmslicenceallocated").focus();
                    $('#dbmslicenceallocated').addClass("is-invalid");

                    return true;
                }
            }
            if ($('#dbstatus').val() == 'Approved') {
                if ($("#dbnewlicencerequestgeneradted").val() == '') {
                    alert('Please Sleect of New Licence Procurement Request Generated');
                    $("#dbnewlicencerequestgeneradted").focus();
                    $('#dbnewlicencerequestgeneradted').addClass("is-invalid");

                    return true;
                }
            }

            var objdata = {

                Remark: $("#txtremarkinfra").val(),
                UserUniqueID: rid,
                NewLicence_Prucure: $("#dbnewlicencerequestgeneradted").val(),
                MS_LicenceAllocated: $("#dbmslicenceallocated").val(),
                ITEmailCreated: $("#newemailid").val(),
                Status: $("#dbstatus").val(),
            };

            $('.spinner').css('display', 'block').fadeIn();
            $("#btnsubmitittransfer").prop("disabled", true);  // Disable the button
            $("#btnsubmitittransfer").text("Please Wait");
            
            $.ajax({
                url: "/Internal_Transfer/VAlidate_By_Infra_Transfercase",
                type: 'POST',
                dataType: 'json', // Expecting JSON response
                contentType: 'application/json; charset=utf-8', // Sending JSON data

                //data: { UniqueID:$("#txtemailid").val(),EmailId:$("#txtemailid").val() },
                data: JSON.stringify(objdata),
                success: function (data) {


                    if (data.flag === 'SUCCESS') {

                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Internal_Transfer/Bind_ON_Infra");
                        $('.spinner').css('display', 'block').fadeOut();
                        // Handle success scenario
                        // e.g., display a success message or redirect
                    } else {
                        $('#modal-Tracking').modal('hide');
                        // Handle failure scenario
                        createRoleSuccess(data.flag, data.message, "/Internal_Transfer/Bind_ON_Infra");
                         $('.spinner').css('display', 'block').fadeOut();
                    }
                },
                error: function (xhr, status, error) {
                    // Handle AJAX error
                    console.error('AJAX Error:', status, error);
                    // Optionally hide the spinner
                    // $('.spinner').css('display', 'block').fadeOut();
                }
            });

        }
    });

}

//-------------------------End By_INRA_ForInternal Transfer----------------------------------------------------------------------------------------------------



$("#dbchoosedomain").change(function () {

    var domainvalue = $("#dbchoosedomain").val();
    var firstName = $("#fname").val(); // Get the first name from input
    var lastName = $("#Lname").val();   // Get the last name from input

    // Create the email by concatenating first name, last name, and domain
    var email = firstName +'.'+ lastName + "@" + domainvalue;

    // Convert the email to lowercase
    email = email.toLowerCase();

    // Set the value of the email input field
    $("#txtproposedemailid").val(email);
})

//----------------------------Login Details ------------------------------------------------------
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
                url: "/Inter/Authentation",

                type: 'POST',
                data: { Username: $("#username").val(), Password: $("#password").val() },
                success: function (data) {
                    var list = data;

                    if (list.flag == 'error') {



                    } else {

                        redirectToPage(list.url);
                    }



                }
            });

        }
    })
}


function LNTlogin() {

    $('#js-lntlogin').validate({ // initialize the plugin
        rules: {
          
            txtotp: "required",
             txtemailid: {
                required: true,
                email: true
            }
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
                url: "/Auth/Authentation_byLIT",

                type: 'POST',
                data: { Username: $("#txtemailid").val(), OTP: $("#txtotp").val() },
                success: function (data) {
                    var list = data;
                    alert(list.flag);
                    if (list.flag == 'error') {

                        alert(list.message);

                    } else {

                        redirectToPage(list.url);
                    }



                }
            });

        }
    })
}
function redirectToPage(redirectUrl) {

    window.location.href = redirectUrl;
}
//----------------------------End Login Details ----------------------------------------------------

//End popup here 









//*--------------------------------- End Add User  Section  -----------------------------------------------------*//