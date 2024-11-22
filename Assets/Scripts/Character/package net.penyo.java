package net.penyo.signin;

import net.penyo.signin.q241030.KTree;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;

import java.math.BigInteger;
import java.time.Duration;
import java.util.Map;
import java.util.stream.Stream;

/**
 * 现存在一种特别的 k 叉树，k 总是与节点所处的层数有关。如：第一层每个节点（根节点）只有一个子节点；
 * 第二层每个节点有两个子节点；第三层每个节点有三个子节点......现将全体正整数按顺序填入该 k 叉树，
 * 请设计静态方法判断系统给出的数（必是正整数）在树第几层第几位（都从 1 开始计数而不是从 0）。
 *
 * <p>
 * 请控制算法的耗时，单次高于 50 毫秒将被视为错误。
 *
 * @author Penyo
 */


public class KTree {
    int a=data;
    for(int i=0;i<50;i++){
        for
    }
}


public class Q241030 {

    @ParameterizedTest
    @MethodSource("data")
    public void checker(String input, Map<?, ?> expected) {
        Assertions.assertTimeout(Duration.ofMillis(50), () -> Assertions.assertEquals(
                expected, KTree.getPositionInTheTree(new BigInteger(input)),
                "你的结果不对。问题出在算法上。"
        ), "你执行的太慢了。别太依赖循环！");
    }

    private static Stream<Arguments> data() {
        return Stream.of(
                Arguments.of("22302091020", Map.of(206408,93986))
        );
    }

}
