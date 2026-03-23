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

//function Save_Loan_Request() {

//    $('#Loan_Request').validate({
//        rules: {
//            txtname: "required",
//            txtempcode: "required",
//            txtdepartment: "required",
//            txtdatejoin: "required",
//            txtsalarydown: "required",
//            txtrupess: "required",
//            dbdeviation: "required",
//            txtinstallments: "required",
//            dbpurpos: "required",
//            textpurpos: "required",
//            txtremark: "required"
//        },
//        submitHandler: function (form) {

//            // Validate Guarantees
//            if ($('#dbgarantee1').val() == '') {
//                alert('Please Select Garanteed 1 List');
//                return false;
//            }

//            if ($('#dbgarantee2').val() == '') {
//                alert('Please Select Garanteed 2 List');
//                return false;
//            }

//            var garanteeecode1 = [];
//            $("#dbgarantee1 option:selected").each(function () {
//                garanteeecode1.push($(this).val());
//            });

//            var garanteeecode2 = [];
//            $("#dbgarantee2 option:selected").each(function () {
//                garanteeecode2.push($(this).val());
//            });

//            var formData = new FormData();

//            // Append Model Data
//            formData.append("deviation", $("#dbdeviation").val());
//            formData.append("installments", $("#txtinstallments").val());
//            formData.append("salarydown", $("#txtsalarydown").val());
//            formData.append("rupess", $("#txtrupess").val());
//            formData.append("purpose", $("#dbpurpos").val());
//            formData.append("purposedesc", $("#textpurpos").val());
//            formData.append("Garantees1", garanteeecode1.toString());
//            formData.append("Garantees2", garanteeecode2.toString());
//            formData.append("Remark", $("#txtremark").val());
//            formData.append("Actiontype", $("#btnsubmithrloancreation").text());
//            formData.append("id", $("#hiddenuniqueid").val());
//            formData.append("hiddenimaegurl", $("#hiddenimageurl").val());

//            // Append Files
//            var files = $("#fileupload")[0].files;
//            for (var i = 0; i < files.length; i++) {
//                formData.append("files", files[i]);
//            }

//            $('.spinner').show();
//            $("#btnsubmithrloancreation").prop("disabled", true);

//            $.ajax({
//                url: '/loan/Request_Loan_Initiated',
//                type: 'POST',
//                data: formData,
//                processData: false,
//                contentType: false,
//                success: function (data) {

//                    if (data.flag == "1") {
//                        alert(data.message);
//                        window.location.href = "/Loan/Load_Request_Details";
//                    } else {
//                        alert(data.message);
//                    }

//                    $('.spinner').hide();
//                    $("#btnsubmithrloancreation").prop("disabled", false).text("Submit");
//                },
//                error: function () {
//                    alert("Error occurred");
//                    $('.spinner').hide();
//                    $("#btnsubmithrloancreation").prop("disabled", false).text("Submit");
//                }
//            });
//        }
//    });
//}

function Save_Loan_Request() {

    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    var rid = params['id'];

    $('#Loan_Request').validate({
        rules: {
            txtname: "required",
            txtempcode: "required",
            txtdepartment: "required",
            txtdatejoin: "required",
            txtsalarydown: "required",
            txtrupess: "required",
            dbdeviation: "required",
            txtinstallments: "required",
            dbpurpos: "required",
            textpurpos: "required",
            txtremark: "required"
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

        highlight: function (element) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },

        unhighlight: function (element) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },

        submitHandler: function (form) {

            // ---------------------------
            // Custom Validations
            // ---------------------------

            if ($('#dbgarantee1').val() == '') {
                alert('Please Select Garanteed 1 List');
                $("#dbgarantee1").focus().addClass("is-invalid");
                return false;
            }

            if ($('#dbgarantee2').val() == '') {
                alert('Please Select Garanteed 2 List');
                $("#dbgarantee2").focus().addClass("is-invalid");
                return false;
            }

            // ---------------------------
            // Get selected guarantees
            // ---------------------------

            var garanteeecode1 = [];
            $("#dbgarantee1 option:selected").each(function () {
                garanteeecode1.push($(this).val());
            });

            var garanteeecode2 = [];
            $("#dbgarantee2 option:selected").each(function () {
                garanteeecode2.push($(this).val());
            });

            // ---------------------------
            // File Validation
            // ---------------------------

            var fileInput = document.getElementById('fileupload');
            var files = fileInput.files;
            var totalSize = 0;

            if (files.length > 0) {

                for (var i = 0; i < files.length; i++) {

                    var file = files[i];
                    totalSize += file.size;

                    // Size check (5MB)
                    if (totalSize > 5 * 1024 * 1024) {
                        alert("Maximum allowed file size is 5 MB");
                        return false;
                    }

                    // Extension check
                    var ext = file.name.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ["png", "jpg", "jpeg", "pdf"]) === -1) {
                        alert("Only png, jpg, jpeg and pdf files are allowed.");
                        return false;
                    }
                }

            } else {
               /* if ($("#btnsubmithrloancreation").text() === "Submit") {*/
                    alert("Please upload attachment");
                    return false;
                /*}*/
            }

            // ---------------------------
            // UI Lock (ONLY AFTER VALIDATION)
            // ---------------------------

            $('.spinner').fadeIn();
            $("#btnsubmithrloancreation").prop("disabled", true);

            // ---------------------------
            // FormData
            // ---------------------------

            var formData = new FormData();

            // Append files
            for (var i = 0; i < files.length; i++) {
                formData.append("files", files[i]);
            }

            // Append data
            formData.append("deviation", $("#dbdeviation").val());
            formData.append("installments", $("#txtinstallments").val());
            formData.append("salarydown", $("#txtsalarydown").val());
            formData.append("rupess", $("#txtrupess").val());
            formData.append("purpose", $("#dbpurpos").val());
            formData.append("purposedesc", $("#textpurpos").val());
            formData.append("Garantees1", garanteeecode1.toString());
            formData.append("Garantees2", garanteeecode2.toString());
            formData.append("Remark", $("#txtremark").val());
            formData.append("Actiontype", $("#btnsubmithrloancreation").text());
            formData.append("id", $("#hiddenuniqueid").val());
            formData.append("hiddenimaegurl", $("#hiddenimageurl").val());

            // ---------------------------
            // AJAX Call
            // ---------------------------

            $.ajax({
                url: '/loan/Request_Loan_Initiated',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,

                success: function (data) {

                    if (data.flag == '1') {
                        createRoleSuccess("SUCCESS", data.message, "/Loan/Load_Request_Details");
                    }

                    $('.spinner').fadeOut();
                    $("#btnsubmithrloancreation").prop("disabled", false).text("Submit");
                },

                error: function () {
                    $('.spinner').fadeOut();
                    $("#btnsubmithrloancreation").prop("disabled", false).text("Submit");
                }
            });
        }
    });
}
var checksubmittype = "";
$("#txtremark").on("input", function () {
    if (checksubmittype === 'Rejected' || checksubmittype === 'SentBack') {
        if ($("#txtremark").val() != "") {
            $(this).removeClass("is-invalid");
        } else {

            $(this).addClass("is-invalid");
        }
    } else { $(this).removeClass("is-invalid");}
});

function Loan_Validate_Approve_GCO_HR(selectedAction) {
    checksubmittype = selectedAction;
    if (!confirm(`Are you sure you want to ${selectedAction} this request?`)) {
        return;
    }
    if (selectedAction === 'Approved') {
        $("#txtremark").removeClass("is-invalid");
    }
    // Helper function for required field validation
    function validateRequired(selector) {
        const $el = $(selector);
        if ($el.val().trim() === '') {
            $el.focus().addClass("is-invalid");
            return false;
        }
        $el.removeClass("is-invalid");
        return true;
    }

    // Validate remarks
    if (!validateRequired("#dbbankname")) return;
    if (!validateRequired("#txtifccode")) return;
    if (!validateRequired("#txtbankaccountnumber")) return;
    if (!validateRequired("#txtgcohramount")) return;
    if (!validateRequired("#txtnoofinstalgco")) return;
    if (!validateRequired("#txtpannumber")) return;
    if (!validateRequired("#dbcostcenter")) return;
    if (!validateRequired("#dbsapcostcenter")) return;
    if (!validateRequired("#dbprfofitcenter")) return;
    if (!validateRequired("#dbnewcostcenter")) return;

    if (selectedAction === 'Rejected' || selectedAction === 'SentBack') {
        if ($("#txtremark").val().trim() === '') {
            alert(
                selectedAction === 'Rejected'
                    ? 'Please select a reason for rejection.'
                    : 'Please provide a remark for sending back.'
            );
            $("#txtremark")
                .focus()
                .addClass("is-invalid");
            return;
        }
    }

    $('.spinner').css('display', 'block').fadeIn();
    $("#btnsubmithrloanapprove").prop("disabled", true);  // Disable the button
    $("#btnsubmithrloansentback").prop("disabled", true);  // Disable the button
    $("#btnsubmithrloanreject").prop("disabled", true);  // Disable the button
    var objdata = {
        CStatus: selectedAction,
        Remark: $("#txtremark").val(),
        bankname: $("#dbbankname").val(),
        ifsccode: $("#txtifccode").val(),
        accountnumber: $("#txtbankaccountnumber").val(),
        gcofinalamount: $("#txtgcohramount").val(),
        gcoinstalment: $("#txtnoofinstalgco").val(),
        pan: $("#txtpannumber").val(),
        
        costcenter: $("#dbcostcenter").val(),
        sapcostcenter: $("#dbsapcostcenter").val(),
        prfofitcenter: $("#dbprfofitcenter").val(),
        newcostcenter: $("#dbnewcostcenter").val(),

        id: $("#txtuniqueid").val() // use .val() if it's an input
    };

    $.ajax({
        url: "/Loan/Validate_Loan_Request_GCO_HR",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(objdata),

        success: function (data) {
         

                 createRoleSuccess("SUCCESS", data.message, "/Loan/Approval_Pending");
          
        },

        error: function () {
            // In case of an error, re-enable the button as well
            $("#btnsubmithrloanapprove").prop("disabled", false);
            $("#btnsubmithrloansentback").prop("disabled", false);  // Disable the button
            $("#btnsubmithrloanreject").prop("disabled", false);  // Disable the button
            $('.spinner').fadeOut();
            alert("Something went wrong. Please try again.");
        },

        complete: function () {

            $("#btnsubmithrloanapprove").prop("disabled", false);
            $("#btnsubmithrloansentback").prop("disabled", false);  // Disable the button
            $("#btnsubmithrloanreject").prop("disabled", false);  // Disable the button
            $('.spinner').fadeOut();
        }
    });
}
function Loan_Validate_Approve(selectedAction) {
    checksubmittype = selectedAction;
    if (!confirm(`Are you sure you want to ${selectedAction} this request?`)) {
        return;
    }
    if (selectedAction === 'Approved') {
        $("#txtremark").removeClass("is-invalid");
    }


    // Validate remarks

    if (selectedAction === 'Rejected' || selectedAction === 'SentBack') {
        if ($("#txtremark").val().trim() === '') {
            alert(
                selectedAction === 'Rejected'
                    ? 'Please select a reason for rejection.'
                    : 'Please provide a remark for sending back.'
            );
            $("#txtremark")
                .focus()
                .addClass("is-invalid");
            return;
        }
    }

    $('.spinner').css('display', 'block').fadeIn();
    $("#btnsubmithrloanapprove").prop("disabled", true);  // Disable the button
    $("#btnsubmithrloansentback").prop("disabled", true);  // Disable the button
    $("#btnsubmithrloanreject").prop("disabled", true);  // Disable the button
    var objdata = {
        CStatus: selectedAction,
        Remark: $("#txtremark").val(),
        id: $("#txtuniqueid").val() // use .val() if it's an input
    };

    $.ajax({
        url: "/Loan/Validate_Loan_Request",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(objdata),
     
        success: function (data) {
            createRoleSuccess("SUCCESS", data.message, "/Loan/Approval_Pending");
            //createRoleSuccess(
            //    data.flag,
            //    data.message,
            //    "../Approval_Pending"
            //);
        },

        error: function () {
            // In case of an error, re-enable the button as well
            $("#btnsubmithrloanapprove").prop("disabled", false);
            $("#btnsubmithrloansentback").prop("disabled", false);  // Disable the button
            $("#btnsubmithrloanreject").prop("disabled", false);  // Disable the button
            $('.spinner').fadeOut();
            alert("Something went wrong. Please try again.");
        },

        complete: function () {
     
            $("#btnsubmithrloanapprove").prop("disabled", false);
            $("#btnsubmithrloansentback").prop("disabled", false);  // Disable the button
            $("#btnsubmithrloanreject").prop("disabled", false);  // Disable the button
            $('.spinner').fadeOut();
        }
    });
}


function Loan_Release_by_Finance_Approve(selectedAction) {
    checksubmittype = selectedAction;
    if (!confirm(`Are you sure you want to ${selectedAction} this request?`)) {
        return;
    }
    if (selectedAction === 'Approved') {
        $("#txtremark").removeClass("is-invalid");
    }


    // Validate remarks

    if (selectedAction === 'Rejected' || selectedAction === 'SentBack') {
        if ($("#txtremark").val().trim() === '') {
            alert(
                selectedAction === 'Rejected'
                    ? 'Please select a reason for rejection.'
                    : 'Please provide a remark for sending back.'
            );
            $("#txtremark")
                .focus()
                .addClass("is-invalid");
            return;
        }
    }

    $('.spinner').css('display', 'block').fadeIn();
    $("#btnsubmithrloanapprove").prop("disabled", true);  // Disable the button
    $("#btnsubmithrloansentback").prop("disabled", true);  // Disable the button
    $("#btnsubmithrloanreject").prop("disabled", true);  // Disable the button
    var objdata = {
        CStatus: selectedAction,
        Remark: $("#txtremark").val(),
        id: $("#txtuniqueid").val() // use .val() if it's an input
    };

    $.ajax({
        url: "/Loan/Validate_Finance_Release_Request",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(objdata),

        success: function (data) {
            createRoleSuccess("SUCCESS", data.message, "/Loan/Approval_Pending");
            //createRoleSuccess(
            //    data.flag,
            //    data.message,
            //    "../Approval_Pending"
            //);
        },

        error: function () {
            // In case of an error, re-enable the button as well
            $("#btnsubmithrloanapprove").prop("disabled", false);
            $("#btnsubmithrloansentback").prop("disabled", false);  // Disable the button
            $("#btnsubmithrloanreject").prop("disabled", false);  // Disable the button
            $('.spinner').fadeOut();
            alert("Something went wrong. Please try again.");
        },

        complete: function () {

            $("#btnsubmithrloanapprove").prop("disabled", false);
            $("#btnsubmithrloansentback").prop("disabled", false);  // Disable the button
            $("#btnsubmithrloanreject").prop("disabled", false);  // Disable the button
            $('.spinner').fadeOut();
        }
    });
}

function Loan_Validate_Approve_HR_PADepart(selectedAction) {

    checksubmittype = selectedAction;

    if (!confirm(`Are you sure you want to ${selectedAction} this request?`)) {
        return;
    }

    // Helper function for required field validation
    function validateRequired(selector) {
        const $el = $(selector);
        if ($el.val().trim() === '') {
            $el.focus().addClass("is-invalid");
            return false;
        }
        $el.removeClass("is-invalid");
        return true;
    }

    // Validate mandatory fields
    if (!validateRequired("#txtsalarybasicvdahra")) return;
    if (!validateRequired("#dbwhetherconfirmedornot")) return;
    if (!validateRequired("#txteligibilityamount")) return;
    if (!validateRequired("#dbloanclosedstatus")) return;
    if (!validateRequired("#txtsapemployeeocde")) return;
    if (!validateRequired("#txtnoofinstallmentforreppayment")) return;

    // Validate remarks based on action
    if (selectedAction === 'Rejected' || selectedAction === 'SentBack') {
        if (!validateRequired("#txtremark")) {
            alert(
                selectedAction === 'Rejected'
                    ? 'Please select a reason for rejection.'
                    : 'Please provide a remark for sending back.'
            );
            return;
        }
    } else {
        $("#txtremark").removeClass("is-invalid");
    }
    // Validate remarks based on action
    if ($("#dbloanclosedstatus").val() === 'Yes') {
        if (!validateRequired("#txtloancloseddate")) {
            alert("Please select a Loan Closed Date.");
            return;
        }
    } else {
        $("#dbloanclosedstatus").removeClass("is-invalid");
    }

    $('.spinner').css('display', 'block').fadeIn();
    $("#btnsubmithrloancreation").prop("disabled", true);  // Disable the button
    // ✅ Correct & clean JSON object
    const objdata = {
        CStatus: selectedAction,
        salarybasicvdahra: $("#txtsalarybasicvdahra").val().trim(),
        whetherconfirmedornot: $("#dbwhetherconfirmedornot").val().trim(),
        eligibilityamount: $("#txteligibilityamount").val().trim(),
        noofinstallmentforreppayment: $("#txtnoofinstallmentforreppayment").val().trim(),
        Remark: $("#txtremark").val().trim(),

        loanstatus: $("#dbloanclosedstatus").val().trim(),
        closeddate: $("#txtloancloseddate").val().trim(),
        sapecode: $("#txtsapemployeeocde").val().trim(),

        id: $("#txtuniqueid").val()
    };

    $.ajax({
        url: "/Loan/Validate_Loan_HR_PA_Depart",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(objdata),

        success: function (data) {
            createRoleSuccess("SUCCESS", data.message, "/Loan/Approval_Pending");
        },

        error: function () {
            $('.spinner').css('display', 'block').fadeIn();
            $("#btnsubmithrloancreation").prop("disabled", true);  // Disable the button
            alert("Something went wrong. Please try again.");
        },

        complete: function () {
            $('.spinner').fadeOut();
        }
    });
}


function Loan_Validate_Approve_HR_Deviation(selectedAction) {

    checksubmittype = selectedAction;

    if (!confirm(`Are you sure you want to ${selectedAction} this request?`)) {
        return;
    }

    // Helper function for required field validation
    function validateRequired(selector) {
        const $el = $(selector);
        if ($el.val().trim() === '') {
            $el.focus().addClass("is-invalid");
            return false;
        }
        $el.removeClass("is-invalid");
        return true;
    }

    // Validate mandatory fields
    if (!validateRequired("#txtdeviationsanctionedamount")) return;
    if (!validateRequired("#txtdeviationfinalamount")) return;
    if (!validateRequired("#txtdeviationnoofinstallmentforreppayment")) return;
  

  

    // Validate remarks based on action
    if (selectedAction === 'Rejected' || selectedAction === 'SentBack') {
        if (!validateRequired("#txtremark")) {
            alert(
                selectedAction === 'Rejected'
                    ? 'Please select a reason for rejection.'
                    : 'Please provide a remark for sending back.'
            );
            return;
        }
    } else {
        $("#txtremark").removeClass("is-invalid");
    }

    $('.spinner').fadeIn();

    // ✅ Correct & clean JSON object
    const objdata = {
        CStatus: selectedAction,
        deviationsanctionedamount: $("#txtdeviationsanctionedamount").val().trim(),
        deviationnoofinstallmentforreppayment: $("#txtdeviationnoofinstallmentforreppayment").val().trim(),
        deviationfinalamount: $("#txtdeviationfinalamount").val().trim(),
        Remark: $("#txtremark").val().trim(),
        id: $("#txtuniqueid").val()
    };

    $.ajax({
        url: "/Loan/Validate_Loan_deviationsan",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(objdata),

        success: function (data) {
            //createRoleSuccess(
            //    data.flag,
            //    data.message,
            //    "../Approval_Pending"
            //);
            createRoleSuccess("SUCCESS", data.message, "/Loan/Approval_Pending");
        },

        error: function () {
            alert("Something went wrong. Please try again.");
        },

        complete: function () {
            $('.spinner').fadeOut();
        }
    });
}

function Loan_Validate_Approve_HR_Final(selectedAction) {

    checksubmittype = selectedAction;

    if (!confirm(`Are you sure you want to ${selectedAction} this request?`)) {
        return;
    }

    // Helper function for required field validation
    function validateRequired(selector) {
        const $el = $(selector);
        if ($el.val().trim() === '') {
            $el.focus().addClass("is-invalid");
            return false;
        }
        $el.removeClass("is-invalid");
        return true;
    }

    // Validate mandatory fields
    if (!validateRequired("#txtfinalanctionedamount")) return;
    if (!validateRequired("#txtfinalnoofinstallmentforreppayment")) return;
    if (!validateRequired("#txtfinalamount")) return;



    // Validate remarks based on action
    if (selectedAction === 'Rejected' || selectedAction === 'SentBack') {
        if (!validateRequired("#txtremark")) {
            alert(
                selectedAction === 'Rejected'
                    ? 'Please select a reason for rejection.'
                    : 'Please provide a remark for sending back.'
            );
            return;
        }
    } else {
        $("#txtremark").removeClass("is-invalid");
    }

    $('.spinner').fadeIn();

    // ✅ Correct & clean JSON object
    const objdata = {
        CStatus: selectedAction,
        finalsanctionedamount: $("#txtfinalanctionedamount").val().trim(),
        finalnoofinstallmentforreppayment: $("#txtfinalnoofinstallmentforreppayment").val().trim(),
        finalamount: $("#txtfinalamount").val().trim(),
        Remark: $("#txtremark").val().trim(),
        id: $("#txtuniqueid").val()
    };

    $.ajax({
        url: "/Loan/Validate_Loan_Validate_Loan_Final",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(objdata),

        success: function (data) {
            //createRoleSuccess(
            //    data.flag,
            //    data.message,
            //    "../Approval_Pending"
            //);

            createRoleSuccess("SUCCESS", data.message, "/Loan/Approval_Pending");
        },

        error: function () {
            alert("Something went wrong. Please try again.");
        },

        complete: function () {
            $('.spinner').fadeOut();
        }
    });
}


function Loan_Ack(selectedAction) {

    checksubmittype = selectedAction;

    if (!confirm(`Are you sure you want to ${selectedAction} this request?`)) {
        return;
    }

    // Helper function for required field validation
    function validateRequired(selector) {
        const $el = $(selector);
        if ($el.val().trim() === '') {
            $el.focus().addClass("is-invalid");
            return false;
        }
        $el.removeClass("is-invalid");
        return true;
    }

    // Validate mandatory fields
    if (!validateRequired("#txtreceiptrs")) return;
    if (!validateRequired("#txtcashchequeno")) return;
    if (!validateRequired("#txtreceiveddate")) return;
   




    // Validate remarks based on action
    if (selectedAction === 'Rejected' || selectedAction === 'SentBack') {
        if (!validateRequired("#txtremark")) {
            alert(
                selectedAction === 'Rejected'
                    ? 'Please select a reason for rejection.'
                    : 'Please provide a remark for sending back.'
            );
            return;
        }
    } else {
        $("#txtremark").removeClass("is-invalid");
    }

    $('.spinner').fadeIn();

    // ✅ Correct & clean JSON object
    const objdata = {
        CStatus: selectedAction,
        receipt_rs: $("#txtreceiptrs").val().trim(),
        cash_cheque_no: $("#txtcashchequeno").val().trim(),
        amount_received_date: $("#txtreceiveddate").val().trim(),
       
        
        Remark: $("#txtremark").val().trim(),
        id: $("#txtuniqueid").val()
    };

    $.ajax({
        url: "/Loan/Validate_AKG",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(objdata),

        success: function (data) {
            createRoleSuccess("SUCCESS", data.message, "/Loan/Approval_Pending");
        },

        error: function () {
            alert("Something went wrong. Please try again.");
        },

        complete: function () {
            $('.spinner').fadeOut();
        }
    });
}
function Validate_Loan_Request_Sent_Back() {

    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    const rid = params['id']; // keep only if needed later

    $('#Validate_Loan_Request').validate({
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element.next(".pmd-textfield-focused"));
            }
        },
        highlight: function (element) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },

        submitHandler: function (form) {

            if (!$('#dbgarantee1').val()) {
                alert('Please Select Guaranteed 1 List');
                $('#dbgarantee1').focus().addClass("is-invalid");
                return false;
            }

            if (!$('#dbgarantee2').val()) {
                alert('Please Select Guaranteed 2 List');
                $('#dbgarantee2').focus().addClass("is-invalid");
                return false;
            }

            const objdata = {

                Remark: $("#txtremark").val()
            };

            // UI lock
            $('.spinner').fadeIn();


            $.ajax({
                url: '/loan/Request_Loan_Initiated',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objdata),

                success: function (data) {
                   

                    if (data.flag === '1') {
                        createRoleSuccess("SUCCESS", data.message, "../Load_Request_Details");
                    }

                    $('.spinner').fadeOut();
                    $("#btnsubmithrloancreation")
                        .prop("disabled", false)
                        .text("Submit");
                },

                error: function () {
                    alert("Something went wrong. Please try again.");
                    $('.spinner').fadeOut();
                    $("#btnsubmithrloancreation")
                        .prop("disabled", false)
                        .text("Submit");
                }
            });
        }
    });
}
function Validate_Loan_Request_Reject() {

    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    const rid = params['id']; // keep only if needed later

    $('#Validate_Loan_Request').validate({
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element.next(".pmd-textfield-focused"));
            }
        },
        highlight: function (element) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },

        submitHandler: function (form) {

            if (!$('#dbgarantee1').val()) {
                alert('Please Select Guaranteed 1 List');
                $('#dbgarantee1').focus().addClass("is-invalid");
                return false;
            }

            if (!$('#dbgarantee2').val()) {
                alert('Please Select Guaranteed 2 List');
                $('#dbgarantee2').focus().addClass("is-invalid");
                return false;
            }

            const objdata = {

                Remark: $("#txtremark").val()
            };

            // UI lock
            $('.spinner').fadeIn();


            $.ajax({
                url: '/loan/Request_Loan_Initiated',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objdata),

                success: function (data) {
                   
                    if (data.flag === '1') {
                        createRoleSuccess("SUCCESS", data.message, "../Load_Request_Details");
                    }

                    $('.spinner').fadeOut();
                    $("#btnsubmithrloancreation")
                        .prop("disabled", false)
                        .text("Submit");
                },

                error: function () {
                    alert("Something went wrong. Please try again.");
                    $('.spinner').fadeOut();
                    $("#btnsubmithrloancreation")
                        .prop("disabled", false)
                        .text("Submit");
                }
            });
        }
    });
}