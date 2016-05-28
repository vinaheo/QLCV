
function initSelectNguoiNhan() {
    jQuery('.slNguoiNhans').select2({
        placeholder: "Select a state"
    });
}
initSelectNguoiNhan();


$('#dataTables-example').DataTable({
    responsive: true
});

function initDatepicker() {
    $('.dpNgayBatDau').datepicker({
        format: "dd/mm/yyyy",
        orientation: "top"
    });
    $('.dpNgayKetThuc').datepicker({
        format: "dd/mm/yyyy",
        orientation: "top"
    });
}

initDatepicker();

function checkDate() {
    if ($('#dpNgayKetThuc').val() < $('#dpNgayBatDau').val()) {
        $('#dpNgayKetThuc').val("");
    }
};

function removepc(i) {
    var num = $("#numItem").val();
    $("#pc" + i).remove();
    $("#numItem").val(num - 1);

    var listTenPhanCong = document.getElementsByClassName('tenphancong');
    for (var i = 0; i < listTenPhanCong.length; i++) {
        listTenPhanCong[i].setAttribute("name", "pcs[" + i + "].TENPHANCONG");
        listTenPhanCong[i].setAttribute("id", "txtTenPhanCong" + i);
    }
    var listNoiDungPhanCong = document.getElementsByClassName('noidungphancong');
    for (var i = 0; i < listNoiDungPhanCong.length; i++) {
        listNoiDungPhanCong[i].setAttribute("name", "pcs[" + i + "].NOIDUNG");
        listNoiDungPhanCong[i].setAttribute("id", "txtNoiDungPhanCong" + i);
    }
    var listNgayBatDau = document.getElementsByClassName('ngaybatdau');
    for (var i = 0; i < listNgayBatDau.length; i++) {
        listNgayBatDau[i].setAttribute("name", "pcs[" + i + "].NGAYBATDAU");
        listNgayBatDau[i].setAttribute("id", "txtNgayBatDau" + i);
    }
    var listNgayKetThuc = document.getElementsByClassName('ngayketthuc');
    for (var i = 0; i < listNgayKetThuc.length; i++) {
        listNgayKetThuc[i].setAttribute("name", "pcs[" + i + "].NGAYKETTHUC");
        listNgayKetThuc[i].setAttribute("id", "txtNgayKetThuc" + i);
    }
    var listNguoiNhans = document.getElementsByClassName('nguoinhan');
    for (var i = 0; i < listNguoiNhans.length; i++) {
        listNguoiNhans[i].setAttribute("name", "listIDNguoiNhans["+i+"]");
        listNguoiNhans[i].setAttribute("id", "txtNguoiNhans" + i);
    }
    var listLiPhanCong = document.getElementsByClassName('liPhanCong');
    for (var i = 0; i < listLiPhanCong.length; i++) {
        listLiPhanCong[i].setAttribute("id", "pc" + i);
    }
    var listLinkOpenModal = document.getElementsByClassName('linkOpenModal');
    for (var i = 0; i < listLinkOpenModal.length; i++) {
        listLinkOpenModal[i].setAttribute("data-target", "#modalPhanCong" + i);
    }
    var listBtnRemovePC = document.getElementsByClassName('btnRemovePC');
    for (var i = 0; i < listBtnRemovePC.length; i++) {
        listBtnRemovePC[i].setAttribute("onclick", "removepc(" + i + ")");
    }
    var listModalPhanCong = document.getElementsByClassName('modalPhanCong');
    for (var i = 0; i < listModalPhanCong.length; i++) {
        listModalPhanCong[i].setAttribute("id", "modalPhanCong" + i);
    }
};

function saveEditPC(i) {

    $('#linkTenPhanCong' + i).text($('#txtEditTenPhanCong' + i).val());
    $('#txtTenPhanCong' + i).val($('#txtEditTenPhanCong' + i).val());
    $('#txtNoiDungPhanCong' + i).val($('#txtEditNoiDungPhanCong' + i).val());
    $('#txtNgayBatDau' + i).val($('#dpEditNgayBatDau' + i).val());
    $('#txtNgayKetThuc' + i).val($('#dpEditNgayKetThuc' + i).val());
    $('#txtNguoiNhans' + i).val($('#taEditNguoiNhan' + i).val());
    $('#spanTenPhanCong' + i).text($('#txtEditTenPhanCong' + i).val());
    $('#spanNoiDungPhanCong' + i).text($('#txtEditNoiDungPhanCong' + i).val());
    $('#spanNgayBatDau' + i).text($('#dpEditNgayBatDau' + i).val());
    $('#spanNgayKetThuc' + i).text($('#dpEditNgayKetThuc' + i).val());
    //$('#spanNguoiNhan' + i).text($('#txtEditTenPhanCong' + i).val());
    var listNguoiNhansText = [];
    var listNguoiNhans = $("#taEditNguoiNhan"+i).select2('data');
    for (var j = 0; j < listNguoiNhans.length; j++) {
        listNguoiNhansText.push(listNguoiNhans[j].text);
    }
    $('#spanNguoiNhan'+i).text(listNguoiNhansText.join(', '));
    closeEditPC(i);
    $('#modalPhanCong' + i).modal('toggle');
}

function checkElementExist(list, i) {
    if (list.indexOf(i) != -1) {
        return "selected";
    } else { return ""; }
}

function taTospan(i) {
    var listNguoiNhansText = [];
    var listNguoiNhans = $("#taEditNguoiNhan" + i).select2('data');
    for (var j = 0; j < listNguoiNhans.length; j++) {
        listNguoiNhansText.push(listNguoiNhans[j].text);
    }
    $('#spanNguoiNhan' + i).text(listNguoiNhansText.join(', '));
}

function test() {
    for (var i = 0; i < $("#numItem").val() ; i++) {
        taTospan(i);
    }
}

test();
function openEditPC(i) {
    $("#txtEditTenPhanCong" + i).removeClass('hidden');
    $("#spanTenPhanCong" + i).addClass('hidden');
    $("#txtEditNoiDungPhanCong" + i).removeClass('hidden');
    $("#spanNoiDungPhanCong" + i).addClass('hidden');
    $("#dpEditNgayBatDau" + i).removeClass('hidden');
    $("#spanNgayBatDau" + i).addClass('hidden');
    $("#dpEditNgayKetThuc" + i).removeClass('hidden');
    $("#spanNgayKetThuc" + i).addClass('hidden');
    $("#divEditNguoiNhan" + i).removeClass('hidden');
    $("#spanNguoiNhan" + i).addClass('hidden');
    $('#divBaoCao' + i).addClass('hidden');
    $("#btnSavePC" + i).removeClass('hidden');
    $("#btnEditPC" + i).addClass('hidden');
}

function closeEditPC(i) {
    $("#txtEditTenPhanCong" + i).addClass('hidden');
    $("#spanTenPhanCong" + i).removeClass('hidden');
    $("#txtEditNoiDungPhanCong" + i).addClass('hidden');
    $("#spanNoiDungPhanCong" + i).removeClass('hidden');
    $("#dpEditNgayBatDau" + i).addClass('hidden');
    $("#spanNgayBatDau" + i).removeClass('hidden');
    $("#dpEditNgayKetThuc" + i).addClass('hidden');
    $("#spanNgayKetThuc" + i).removeClass('hidden');
    $("#divEditNguoiNhan" + i).addClass('hidden');
    $("#spanNguoiNhan" + i).removeClass('hidden');
    $('#divBaoCao' + i).removeClass('hidden');
    $("#btnSavePC" + i).addClass('hidden');
    $("#btnEditPC" + i).removeClass('hidden');
}

$("#btnAdd").click(function () {
    var i = $("#numItem").val();
    if ($('#txtTenPhanCong').val() != "" && $('#txtNoiDungPhanCong').val() != "" && $('#dpNgayBatDau').val() != "" && $('#dpNgayKetThuc').val() != "" && $('#taNguoiNhan').val() != "") {
        var tenphancong = $('#txtTenPhanCong').val();
        var noidungphancong = $('#txtNoiDungPhanCong').val();
        var ngaybatdau = $('#dpNgayBatDau').val();
        var ngayketthuc = $('#dpNgayKetThuc').val();
        //var listNguoiNhansText = [];
        var listIdNguoiNhans = $("#taNguoiNhan").val();
        //var listNguoiNhans = $("#taNguoiNhan").select2('data');
        //for (var j = 0; j < listNguoiNhans.length; j++) {
        //    listNguoiNhansText.push(listNguoiNhans[j].text);
        //}
        //var strNguoiNhans = listNguoiNhansText.join(', ');

        var stringAppend = "<li id='pc" + i + "' class='liPhanCong'>"
                            + "<a data-toggle='modal' data-target='#modalPhanCong" + i + "' id='linkTenPhanCong" + i + "' class='linkOpenModal'>" + tenphancong + "</a>"
                            + "<input name='pcs[" + i + "].TENPHANCONG' value='" + tenphancong + "' class='tenphancong hidden' id='txtTenPhanCong" + i + "'/>"
                            + "<input name='pcs[" + i + "].NOIDUNG' value='" + noidungphancong + "' class='noidungphancong hidden' id='txtNoiDungPhanCong" + i + "'/>"
                            + "<input name='pcs[" + i + "].NGAYBATDAU' value='" + ngaybatdau + "' class='ngaybatdau hidden' id='txtNgayBatDau" + i + "'/>"
                            + "<input name='pcs[" + i + "].NGAYKETTHUC' value='" + ngayketthuc + "' class='ngayketthuc hidden' id='txtNgayKetThuc" + i + "'/>"
                            + "<input name='pcs[" + i + "].IDTRANGTHAI' value='1' class='trangthai hidden' id='txtTrangThai" + i + "'/>"
                            + "<input name='listIDNguoiNhans[" + i + "]' value='" + listIdNguoiNhans + "' class='nguoinhan hidden' id='txtNguoiNhans" + i + "'/>"

                            + "<span style='margin-left: 50px' class='glyphicon glyphicon-trash btnRemovePC' onclick='removepc(" + i + ")' ></span>"
                            + "<div id='modalPhanCong" + i + "' class='modal fade modalPhanCong' role='dialog'>"
                            + "<div class='modal-dialog'>"
                                + "<!-- Modal content-->"
                                + "<div class='modal-content'>"
                                    + "<div class='modal-header'>"
                                        + "<button type='button' class='close' data-dismiss='modal'>&times;</button>"
                                        + "<h4 class='modal-title'>Chi tiết phân công</h4>"
                                    + "</div>"
                                    + "<div class='modal-body'>"
                                        + "<div class='form-group'>"
                                            + "<label>Tên phân công</label>"
                                            + "<input class='form-control txtEditTenPhanCong hidden' id='txtEditTenPhanCong" + i + "' value='" + tenphancong + "'>"
                                            + "<span id='spanTenPhanCong" + i + "' class='spanTenPhanCong'>" + tenphancong + "</span>"
                                        + "</div>"
                                        + "<div class='form-group'>"
                                            + "<label>Nội dung phân công</label>"
                                            + "<textarea class='form-control hidden txtEditNoiDungPhanCong' rows='3' id='txtEditNoiDungPhanCong" + i + "'>" + noidungphancong + "</textarea>"
                                            + "<span id='spanNoiDungPhanCong" + i + "' class='spanNoiDungPhanCong'>" + noidungphancong + "</span>"
                                        + "</div>"
                                        + "<div class='form-group'>"
                                            + "<label>Ngày bắt đầu</label>"
                                            + "<div><input type='text' class='form-control dpNgayBatDau dpEditNgayBatDau hidden' id='dpEditNgayBatDau" + i + "' value='" + ngaybatdau + "' /></div>"
                                            + "<span id='spanNgayBatDau" + i + "' class='spanNgayBatDau'>" + ngaybatdau + "</span>"
                                        + "</div>"
                                        + "<div class='form-group'>"
                                            + "<label>Ngày kết thúc</label>"
                                            + "<div><input type='text' class='form-control dpNgayKetThuc hidden dpEditNgayKetThuc' id='dpEditNgayKetThuc" + i + "' value='" + ngayketthuc + "' /></div>"
                                            + "<span id='spanNgayKetThuc" + i + "' class='spanNgayKetThuc'>" + ngayketthuc + "</span>"
                                        + "</div>"
                                        + "<div class='form-group'>"
                                            + "<label>Phân công cho</label><br/>"
                                            + "<div class='divEditNguoiNhan hidden' id='divEditNguoiNhan"+i+"'>"
                                                + "<select id='taEditNguoiNhan" + i + "' multiple='multiple' class='form-control slNguoiNhans' style='width: 100%'>"
                                                    //+ "<option value='1' " + checkElementExist(listIdNguoiNhans, "1") + ">Quan1</option>"
                                                    //+ "<option value='2' " + checkElementExist(listIdNguoiNhans, "2") + ">Quan2</option>"
                                                    //+ "<option value='3' " + checkElementExist(listIdNguoiNhans, "3") + ">Quan3</option>"
                                                    + "<script>createListNguoiDung(" + i + ",listNguoiDungsByViewBag, $('#taNguoiNhan').val())<\/script>"
                                                + "</select>"
                                                + "<script>initSelectNguoiNhan()<\/script>"
                                            + "</div>"
                                            + "<span id='spanNguoiNhan" + i + "'></span>"
                                        + "</div>"
                                        + "<script>initDatepicker()<\/script>"
                                    + "</div>"
                                    + "<div class='modal-footer'>"
                                    + "<button type='button' class='btn btn-default' id='btnEditPC" + i + "' onclick='openEditPC(" + i + ")'>Edit</button>"
                                        + "<button type='button' class='btn btn-default hidden' id='btnSavePC" + i + "' onclick='saveEditPC(" + i + ")'>Save</button>"
                                        + "<button type='button' class='btn btn-default' data-dismiss='modal'>Close</button>"
                                    + "</div>"
                                + "</div>"
                            + "</div>"
                        + "</div>"
                        + "</li>";

        $("#listchitiet").append(stringAppend);
        var listNguoiNhansText = [];
        var listNguoiNhans = $("#taNguoiNhan").select2('data');
        for (var j = 0; j < listNguoiNhans.length; j++) {
            listNguoiNhansText.push(listNguoiNhans[j].text);
        }

        $('#spanNguoiNhan' + i).text(listNguoiNhansText.join(', '));
        i++;

        $("#numItem").val(i);
        $('#txtTenPhanCong').val("");
        $('#txtNoiDungPhanCong').val("");
        $('#dpNgayBatDau').val("");
        $('#dpNgayKetThuc').val("");
        $('#dpNgayKetThuc').val("");
        $("#taNguoiNhan").val(null).trigger('change.select2');
        $('#modalAddPhanCong').modal('toggle');
    } else {
        alert("Nhập đầy đủ thông tin!");
    };
});

function displayEdit() {
    $('#detailTieuDe').addClass('hidden');
    $('#detailNoiDung').addClass('hidden');
    $('#tieude').removeClass('hidden');
    $('#noidung').removeClass('hidden');
    $('#panelThemChiTiet').removeClass('hidden');
    $('.btnEditPC').removeClass('hidden');
    $('.divBaoCao').addClass('hidden');
    $('.btnRemovePC').removeClass('hidden');
    $('.rowButton').removeClass('hidden');
    $('#btnEditTask').removeClass('hidden');
    $('#uploadPlace').removeClass('hidden');
}
$('.btnCancel').click(function (e) {
    e.preventDefault();
    $('#tieude').addClass('hidden');
    $('#noidung').addClass('hidden');
    $('#panelThemChiTiet').addClass('hidden');
    $('.btnEditPC').addClass('hidden');
    $('.divBaoCao').removeClass('hidden');
    $('.btnaddPC').addClass('hidden');
    $('.rowButton').addClass('hidden');
    $('#btnEditTask').addClass('hidden');
    $('.btnRemovePC').addClass('hidden');
    $('#uploadPlace').addClass('hidden');
});
function closeEdit() {
    $('#tieude').addClass('hidden');
    $('#noidung').addClass('hidden');
    $('#panelThemChiTiet').addClass('hidden');
    $('.btnEditPC').addClass('hidden');
    $('.divBaoCao').removeClass('hidden');
    $('.btnaddPC').addClass('hidden');
    $('.rowButton').addClass('hidden');
    $('#btnEditTask').addClass('hidden');
}

function createListNguoiDung(i, list, listSelected) {
    for (var j = 0; j < list.length; j++) {
        if (listSelected.indexOf(list[j].userId.toString()) != -1) {
            $('#taEditNguoiNhan' + i).append("<option value='" + list[j].userId + "' selected>" + list[j].userTen + "</option>");
        } else {
            $('#taEditNguoiNhan' + i).append("<option value='" + list[j].userId + "'>" + list[j].userTen + "</option>");
        }
    }
}

function CloseTask(idCongViec) {
    var r = confirm("Bạn có đồng ý đóng công việc không?");
    if (r == true) {
        $.ajax({
            type: "POST",
            url: "../HoanThanhCongViec",
            data: JSON.stringify({ idCongViec: idCongViec }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if(data === true){
                    alert("Đã đóng công việc");
                    $('#btnCloseTask').addClass('hidden');
                    $('#btnOpenTask').removeClass('hidden');
                } else {
                    alert(data);
                }
            },
            failure: function (response) {
                alert('Đã có lỗi xảy ra!');
            }
        });
    }
}

function OpenTask(idCongViec) {
    var r = confirm("Bạn có đồng ý mở công việc không?");
    if (r == true) {
        $.ajax({
            type: "POST",
            url: "../HoanThanhCongViec",
            data: JSON.stringify({ idCongViec: idCongViec }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data === true) {
                    alert("Đã mở công việc");
                    $('#btnOpenTask').addClass('hidden');
                    $('#btnCloseTask').removeClass('hidden');
                } else {
                    alert(data);
                }
            },
            failure: function (response) {
                alert('khong thanh cong');
            }
        });
    }
}

function FilterTask() {
    var ids = $('#slId').val();
    $.ajax({
        type: "POST",
        url: "../Report/GetTask",
        data: JSON.stringify({ id: 1 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert('thanh cong');
        },
        failure: function (response) {
            alert('khong thanh cong');
        }
    });
}