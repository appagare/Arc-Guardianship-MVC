if (!Modernizr.inputtypes.date) {
    $(function () {
        $("#DOB").datepicker({
            changeMonth: true,
            changeYear: true,
            clearBtn: true,
            startView: "decade"
        });
    });
}


