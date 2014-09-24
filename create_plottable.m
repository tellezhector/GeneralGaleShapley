function void = create_plottable()
    [data, l] = read_data();
    means = mean(data);
    i  = 3*(0:(l-1));
    n = means(i+1);
    iterations = means(i+2);
    y = [n; iterations; n; n; n; n .* n];
    dlmwrite("plottable.txt", y', " ");
endfunction
