// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function() {

    let screen = $('.calculator-screen');
    let results = $('.calculator-results');

    $('.all-clear').on('click', function() {
        screen.val('');
        results.val('');
    });

    $('.clear').on('click', function() {
        let append = screen.val();
        screen.val(append.substring(0, append.length - 1));
    });


    $('.equal-sign').on('click', function() {
        let prepare = screen.val();

        $.ajax({
            url: "/Home/Callculator/hit",
            type: "POST",
            dataType: "json",
            data: {
                strings: prepare
            },
            success: function(data) {
                results.val(data.val);
            },
            error: function(jxhr, error, code) {

            }
        });
    });

    $('.number').on('click', function() {
        let append = screen.val() + $(this).attr('value');
        screen.val(append);
    });

});