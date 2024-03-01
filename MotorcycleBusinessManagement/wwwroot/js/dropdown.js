//$(document).ready(function () {

//    // Chỉ hiển thị dữ liệu đầu tiên của dropdown list trong thanh tìm kiếm.
//    var firstOption = $("#productDropdown option:first");
//    var initialTenLoaiXe = firstOption.data("tenlx");

//    $("#searchInput").val(firstOption.text());
//    $("#tenLoaiXe").val(initialTenLoaiXe);


//    $('#productDropdown').on('click', 'option', function (event) {
//        event.stopPropagation();
//        var selectedOption = $(this);
//        $('#searchInput').val(selectedOption.text()); // Cập nhật giá trị của input khi chọn sản phẩm
//        $('#tenLoaiXe').val(selectedOption.data("tenlx")); // Cập nhật tên loại xe
//        $('#productList').hide(); // Ẩn danh sách sau khi chọn sản phẩm
//    });

//    $('#searchInput').on('input', function () {
//        var inputText = $(this).val().toLowerCase();
//        $('#productDropdown option').each(function () {
//            var optionText = $(this).text().toLowerCase();
//            var isVisible = optionText.includes(inputText);
//            $(this).toggle(isVisible);
//        });
//    });

//    $("#searchInput").on("keydown", function (e) {
//        var currentOption = $('#productDropdown option:selected');

//        if (e.key === "ArrowDown" || e.key === "ArrowUp") {
//            if (e.key === "ArrowDown") {
//                currentOption.nextAll("option:visible:first").prop("selected", true);
//            } else if (e.key === "ArrowUp") {
//                currentOption.prevAll("option:visible:first").prop("selected", true);
//            }
//            $('#searchInput').val($('#productDropdown option:selected').text());
//            $('#tenLoaiXe').val($('#productDropdown option:selected').data("tenlx"));
//        }
//    });

//    // Khi người dùng nhấp vào thanh tìm kiếm, hãy hiển thị danh sách dropdown list.
//    $("#searchInput").on("focus", function () {
//        $("#productList").show();
//    });

//    // Khi người dùng thoát khỏi thanh tìm kiếm, hãy ẩn danh sách dropdown list.
//    $("#productList").on("blur", function () {
//        setTimeout(function () {
//            $("#productList").hide();
//        }, 200);
//    });
//});

$(document).ready(function () {
    $('#productDropdown').on('click', 'option', function (event) {
        event.stopPropagation();
        var selectedOption = $(this);
        $('#searchInput').val(selectedOption.text()); // Cập nhật giá trị của input khi chọn sản phẩm
        $('#tenNhaCungCap').val(selectedOption.data("tenlx")); // Cập nhật tên nhà cung cấp
        $('#diaChi').val(selectedOption.data("diachincc")); // Cập nhật địa chỉ nhà cung cấp
        $('#productList').hide(); // Ẩn danh sách sau khi chọn sản phẩm
    });

    $('#searchInput').on('input', function () {
        var inputText = $(this).val().toLowerCase();
        $('#productDropdown option').each(function () {
            var optionText = $(this).text().toLowerCase();
            var isVisible = optionText.includes(inputText);
            $(this).toggle(isVisible);
        });
    });

    $("#searchInput").on("keydown", function (e) {
        var currentOption = $('#productDropdown option:selected');

        if (e.key === "ArrowDown" || e.key === "ArrowUp") {
            if (e.key === "ArrowDown") {
                currentOption.nextAll("option:visible:first").prop("selected", true);
            } else if (e.key === "ArrowUp") {
                currentOption.prevAll("option:visible:first").prop("selected", true);
            }
            $('#searchInput').val($('#productDropdown option:selected').text());
            $('#tenNhaCungCap').val($('#productDropdown option:selected').data("tenlx"));
            $('#diaChi').val($('#productDropdown option:selected').data("diachincc"));
        }
    });

    // Khi người dùng nhấp vào thanh tìm kiếm, hãy hiển thị danh sách dropdown list.
    $("#searchInput").on("focus", function () {
        $("#productList").show();
    });

    // Khi người dùng thoát khỏi thanh tìm kiếm, hãy ẩn danh sách dropdown list.
    $("#productList").on("blur", function () {
        setTimeout(function () {
            $("#productList").hide();
        }, 200);
    });
});
