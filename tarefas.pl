use strict;
use warnings;
use JSON;

my $arquivo_tarefas = 'tarefas.json';

sub carregar_tarefas {
    my $tarefas = [];
    if (-e $arquivo_tarefas) {
        open(my $fh, '<', $arquivo_tarefas) or die "Não foi possível abrir o arquivo '$arquivo_tarefas' $!";
        my $json_str = do { local $/; <$fh> };
        close($fh);
        $tarefas = decode_json($json_str);
    }
    return $tarefas;
}

sub salvar_tarefas {
    my ($tarefas) = @_;
    open(my $fh, '>', $arquivo_tarefas) or die "Não foi possível abrir o arquivo '$arquivo_tarefas' $!";
    print $fh encode_json($tarefas);
    close($fh);
}

sub adicionar_tarefa {
    my ($tarefa) = @_;
    my $tarefas = carregar_tarefas();
    push @$tarefas, $tarefa;
    salvar_tarefas($tarefas);
}

sub concluir_tarefa {
    my ($id_tarefa) = @_;
    my $tarefas = carregar_tarefas();
    if ($id_tarefa >= 0 && $id_tarefa < scalar(@$tarefas)) {
        $tarefas->[$id_tarefa]->{concluida} = 1;
        salvar_tarefas($tarefas);
    }
}

sub listar_tarefas {
    my $tarefas = carregar_tarefas();
    foreach my $i (0 .. scalar(@$tarefas) - 1) {
        my $tarefa = $tarefas->[$i];
        my $status = $tarefa->{concluida} ? 'Concluída' : 'Pendente';
        print "$i. $tarefa->{titulo} ($status)\n";
    }
}



print "Gerenciador de Tarefas\n\n";
print "1. Adicionar Tarefa\n";
print "2. Concluir Tarefa\n";
print "3. Listar Tarefas\n";
print "0. Sair\n\n";

while (1) {
    print "Digite sua escolha: ";
    my $escolha = <>;
    chomp($escolha);

    if ($escolha == 0) {
        last;
    }
    elsif ($escolha == 1) {
        print "Digite o título da tarefa: ";
        my $titulo = <>;
        chomp($titulo);
        adicionar_tarefa({ titulo => $titulo, concluida => 0 });
        print "Tarefa adicionada com sucesso!\n";
    }
    elsif ($escolha == 2) {
        print "Digite o ID da tarefa a ser concluída: ";
        my $id_tarefa = <>;
        chomp($id_tarefa);
        concluir_tarefa($id_tarefa);
        print "Tarefa marcada como concluída!\n";
    }
    elsif ($escolha == 3) {
        print "\n";
        listar_tarefas();
        print "\n";
    }
    else {
        print "Escolha inválida. Por favor, tente novamente.\n";
    }
}
