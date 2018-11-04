$.fn.serializeObject = function (generateEmpty) {
	var o = {};
	var a = this.serializeArray();
	$.each(a, function () {
		if (this.value) {
			if (o[this.name]) {
				if (!o[this.name].push) {
					o[this.name] = [o[this.name]];
				}

				console.log(this.value);
				o[this.name].push(this.value || '');
			} else {
				o[this.name] = this.value || '';
			}
		}
	});
	return o;
};

$.fn.valDecimalLimpo = function () {
	var valorLimpo = this.val().replace(/[\R$ .]/g, "");

    return valorLimpo;
};