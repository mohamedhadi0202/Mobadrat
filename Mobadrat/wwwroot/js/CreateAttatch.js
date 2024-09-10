$(document).ready(function () {
	function uploadFilePopUp() {
		$('#FileUploadPopup').bind('change', function () {
			var filename = $("#FileUploadPopup").val();
			if (/^\s*$/.test(filename)) {
				$(".file-upload-popup").removeClass('active');
				$("#noFilePopup").text("لا يوجد ملفات مرفقة ...");
			}
			else
			{
				$(".file-upload-popup").addClass('active');
				$("#noFilePopup").text(filename.replace("C:\\fakepath\\", ""));
				//$('.file-select-button').text('أرفع الملف');
			}
		});
	}
	uploadFilePopUp();

	$("#SubmitAttatchForm").on("click", function () {
		var filename = $("#FileUploadPopup").val();
		var extension = filename.substring(filename.lastIndexOf('.') + 1);

		if ($('#FileUploadPopup').val() == '') {
			$(".validLabelAttach").text("يجب اختيار الملف أولاً لتحميله");
			$(".validLabelAttach").slideDown();
			return false
		}
		else {
			$(".validLabelAttach").slideUp();
		}

		if (extension != "pdf")
		{
			$(".validLabelAttach").text("تحميل الملفات يقبل فقط صيغة PDF");
			$(".validLabelAttach").slideDown();
			return false
		}
	});
});