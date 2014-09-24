function [data,l] = read_data()
    x = dlmread("./results.txt", " ");
    l = x(end, 1);
    x = x(x(:,1) < l, :);
    l = l - 1;
    data = [];
    for i = 0:(l-1)
        index = (1:1000) + (1000 * i); 
        data = [data, x(index, 1:3)];
    endfor
endfunction
