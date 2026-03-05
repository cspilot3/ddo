$.fn.alphanumeric = function (p) {
    return this.each(function () {
        var input = $(this),
            az = "abcdefghijklmnopqrstuvwxyz",
                options = $.extend({
                    ichars: '!@#$%^&*()+=[]\\\';,/{}|":<>?~`.- _¨´áéíóúÁÉÍÓÚ',
                    nchars: '',
                    allow: ''
                }, p),
            s = options.allow.split(''),
            i = 0,
            ch,
            regex;

        for (i; i < s.length; i++) {
            if (options.ichars.indexOf(s[i]) != -1) {
                s[i] = '\\' + s[i];
            }
        }

        if (options.nocaps) {
            options.nchars += az.toUpperCase();
        }
        if (options.allcaps) {
            options.nchars += az;
        }

        options.allow = s.join('|');

        regex = new RegExp(options.allow, 'gi');
        ch = (options.ichars + options.nchars).replace(regex, '');
        //alert("ch = " + ch )

        input.keypress(function (e) {
            var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);

            if (ch.indexOf(key) != -1 && !e.ctrlKey) {
                e.preventDefault();
            }
        });

        //keonglah : Modifying reference to support cut n paste
        //Start to validate the string again when text onblur (cusor move to other point)
        input.blur(function () {
            var value = input.val(), // get current text entry

            j = 0;

            final_string = '' // setup final string
            for (j; j < value.length; j++) { // Check string one by one
                char_entry = value.charAt(j);  //
                if (ch.indexOf(char_entry) < 0) { // mean -1, not match those invalid string
                    final_string = final_string + char_entry; // joint all string together
                }
                input.val(final_string); // set to the input string

            }
            return false;
        });
    });
};

$.fn.texto = function (estricto) {
    return this.each(function () {
        if (estricto === true) {
            $(this).alphanumeric({ allow: ' ' });
        } else {
            $(this).alphanumeric({ allow: ' -;¿¬()!$:,.*()+=[]áéíóúÁÉÍÓÚ' });
        }
    });
};
