$(function () {
    var formData = {
        email: "",
        phoneNumber: ""
    };

    $("#login-form").dxForm({
        formData: formData,
        items: [
            {
                dataField: "email",
                label: { text: "Email" },
                editorType: "dxTextBox",
                editorOptions: { placeholder: "Nhập email..." }
            },
            {
                dataField: "phoneNumber",
                label: { text: "Số điện thoại" },
                editorType: "dxTextBox",
                editorOptions: { placeholder: "Nhập số điện thoại..." }
            }
        ]
    });

    $("#login-button").dxButton({
        text: "Đăng nhập",
        type: "success",
        width: "100%",
        onClick: function () {
            var data = $("#login-form").dxForm("instance").option("formData");

            if (!data.email || !data.phoneNumber) {
                $("#error-message").text("Vui lòng nhập email và số điện thoại.");
                return;
            }

            var button = $("#login-button").dxButton("instance");
            button.option("disabled", true);
            button.option("text", "Đang đăng nhập...");

            $.ajax({
                url: "/api/auth/login",
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify(data),
                success: function (response) {
                    window.location.href = "/Home/Index";
                },
                error: function () {
                    $("#error-message").text("Sai email hoặc số điện thoại.");
                },
                complete: function () {
                    button.option("disabled", false);
                    button.option("text", "Đăng nhập");
                }
            });
        }
    });
});
